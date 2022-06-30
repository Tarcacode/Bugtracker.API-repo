﻿using Bugtracker.API.BLL.DataTransferObjects;
using Bugtracker.API.BLL.Interfaces;
using Bugtracker.API.BLL.Mappers;
using Bugtracker.API.DAL.Entities;
using Bugtracker.API.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugtracker.API.BLL.Services
{
    public class MemberService : IMemberService
    {
        private IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public IEnumerable<MemberDto> GetAll()
        {
            IEnumerable<MemberEntity> all = _memberRepository.GetAll();
            return (all is DBNull) ? null : all.Select(member => member.ToDto());
        }
        public MemberDto GetByLogin(string login)
        {
            MemberDto member = _memberRepository.GetByLogin(login).ToDto();
            return member;
        }
        public MemberDto Insert(MemberDto member)
        {
            int? idMember = _memberRepository.Insert(member.ToEntity());
            member.IdMember = (idMember is null) ? 0 : (int)idMember;
            return member;
        }

        public bool Delete(int id)
        {
            return _memberRepository.Delete(id);
        }

    }
}
