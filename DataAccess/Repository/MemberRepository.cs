using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void AddNew(TblMember member)
        {
            MemberDAO.Instance.AddNew(member);
        }

        public TblMember GetMembersById(int Id)
        {
            return MemberDAO.Instance.GetMemberByID(Id);
        }

        public IEnumerable<TblMember> GetMembersList()
        {
            return MemberDAO.Instance.GetMemberList();
        }

        public IEnumerable<TblMember> GetMembersListByUser(int memberId)
        {
            return MemberDAO.Instance.GetMemberListByUser(memberId);
        }

        public void Remove(int memberId)
        {
            MemberDAO.Instance.Remove(memberId);
        }

        public void Update(TblMember member)
        {
            MemberDAO.Instance.Update(member);
        }
    }
}
