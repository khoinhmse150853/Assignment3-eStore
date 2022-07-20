using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<TblMember> GetMembersList(string cs);   
        TblMember GetMembersById(string cs, int Id);
        IEnumerable<TblMember> SearchMembers(string cs, int id);
        void Update(string cs, TblMember member);
        void AddNew(string cs, TblMember member);
        void Remove(string cs, int memberId);
    }
}
