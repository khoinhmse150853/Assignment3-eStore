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
        public void AddNew(string cs, TblMember member)
        {
            MemberDAO.Instance.AddNew(cs, member);
        }

        public TblMember GetMembersById(string cs, int Id)
        {
            return MemberDAO.Instance.GetMemberByID(cs, Id);
        }

        public IEnumerable<TblMember> GetMembersList(string cs)
        {
            return MemberDAO.Instance.GetMembersList(cs);
        }

        public void Remove(string cs, int memberId)
        {
            MemberDAO.Instance.Remove(cs, memberId);
        }

        public IEnumerable<TblMember> SearchMembers(string cs, int id)
        {
            return MemberDAO.Instance.SearchMembers(cs, id);
        }


        public void Update(string cs, TblMember member)
        {
            MemberDAO.Instance.Update(cs, member);
        }
    }
}
