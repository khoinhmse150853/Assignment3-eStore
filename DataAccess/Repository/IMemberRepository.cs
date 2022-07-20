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
        IEnumerable<TblMember> GetMembersList();
        IEnumerable<TblMember> GetMembersListByUser(int memberId);
        TblMember GetMembersById(int Id);
        void Update(TblMember member);
        void AddNew(TblMember member);
        void Remove(int memberId);
    }
}
