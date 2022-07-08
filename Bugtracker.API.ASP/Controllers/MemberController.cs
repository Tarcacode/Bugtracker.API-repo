﻿using Bugtracker.API.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bugtracker.API.BLL.DataTransferObjects;
using System.Diagnostics.Metrics;
using Isopoh.Cryptography.Argon2;
using System;
using Bugtracker.API.ASP.ApiModels.MemberApiModels;
using Bugtracker.API.BLL.Tools;
using Microsoft.AspNetCore.Authorization;
using Bugtracker.API.ASP.ApiMappers;

namespace Bugtracker.API.ASP.Controllers
{
    [Authorize("isConnected")]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private IMemberService _memberService;
   
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<MemberDto> allMembers = _memberService.GetAll();
            if (allMembers.Count() == 0)
                return NotFound("Members list empty or not found.");
            else
                return Ok(allMembers);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute]int id)
        {
            MemberDto memberDto = _memberService.GetById(id);
            if (memberDto is null)
                return NotFound("Member id not found.");
            else
                return Ok(memberDto);
        }


        // TODO : Vérifier si j'ai vraiment besoin de récupérer l'id depuis la route
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Remove([FromRoute]int id)
        {
            bool isMemberRemoved = _memberService.Remove(id);
            if (!isMemberRemoved)
                return BadRequest("Member id not found.");
            else
                return NoContent();
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Edit([FromRoute] int id, MemberEditModel memberEdit)
        {
            try
            {
                bool isEdited = _memberService.Edit(memberEdit.ToEditDto());
                if (!isEdited)
                    return NotFound("Member id not found.");
                else
                    return NoContent();
            }
            catch (MemberException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("token")]
        public IActionResult TokenValidation(string token)
        {
            if (token is null)
                return NotFound("Token not found.");
            else
                return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Add(MemberDto memberDto)
        {
            try
            {
                int idMember = _memberService.Add(memberDto);
                memberDto.IdMember = idMember;
                return new CreatedResult("/api/Member", memberDto);
            }
            catch (MemberException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult TryToLogin(MemberLoginModel loginModel)
        {
            try
            {
                ConnectedMemberDto connectedMember = _memberService.TryToLogin(loginModel.ToDto());
                return Ok(connectedMember);
            }
            catch (MemberException exception) 
            {
                if (exception.Message.Contains("Member pseudo not found."))
                    return NotFound(exception.Message);
                else
                    return BadRequest(exception.Message);
            }
        }
        
        
    }
    
}
