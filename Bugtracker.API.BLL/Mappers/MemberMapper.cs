using Bugtracker.API.BLL.DataTransferObjects;
using Bugtracker.API.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugtracker.API.BLL.Mappers
{
    internal static class MemberMapper
    {
        public static MemberDto ToDto(this MemberEntity entity)
        {
            return new MemberDto()
            {
                IdMember = entity.IdMember,
                Pseudo = entity.Pseudo,
                Email = entity.Email,
                PswdHash = entity.PswdHash,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Activated = entity.Activated
            };
        }

        public static MemberEntity ToEntity(this MemberDto dto)
        {
            return new MemberEntity()
            {
                IdMember = dto.IdMember,
                Pseudo = dto.Pseudo,
                Email = dto.Email,
                PswdHash = dto.PswdHash,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Activated = dto.Activated
            };
        }
        public static MemberPostEntity ToPostEntity(this MemberPostDto postDto)
        {
            return new MemberPostEntity()
            {
                Pseudo = postDto.Pseudo,
                Email = postDto.Email,
                PswdHash = postDto.Password
            };
        }
    }
}
