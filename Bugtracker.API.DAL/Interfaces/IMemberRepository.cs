using Bugtracker.API.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugtracker.API.DAL.Interfaces
{
    public interface IMemberRepository
    {
        bool MemberPseudoExist(string pseudo);
        bool MemberPseudoExistWithId(string pseudo, int id);
        bool MemberEmailExist(string email);
        bool MemberEmailExistWithId(string email, int id);
        MemberEntity GetByPseudo(string pseudo);
        int Register(MemberPostEntity postEntity);
        IEnumerable<MemberEntity> GetAll();
        MemberEntity GetById(int id);
        bool Edit(MemberEntity entity);
        bool Remove(int id);
        bool ChangePswd(int id, string pswd);
    }
}
