using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;
        private static object instanceLook = new object();

        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public List<TblMember> GetMembersList(string cs)
        {
            using (var db = new SaleManagementContext(cs))
            {
                return db.TblMembers.ToList();
            }
        }

        public TblMember GetMemberByID(string cs, int memberId)
        {
            TblMember member = null;
            try
            {
                using (var db = new SaleManagementContext(cs))
                {
                    member = db.TblMembers.SingleOrDefault(m => m.MemberId == memberId);
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        public List<TblMember> SearchMembers(string cs, int id)
        {
            List<TblMember> members = null;
            try
            {
                using (var db = new SaleManagementContext(cs))
                {
                    members = db.TblMembers.Where(m => m.MemberId.Equals(id)).ToList();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return members;
        }

        public void Update(string cs, TblMember member)
        {
            try
            {
                TblMember _member = GetMemberByID(cs, member.MemberId);
                using (var db = new SaleManagementContext(cs))
                {
                    if (_member != null)
                    {
                        db.TblMembers.Update(member);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The member does not already exists");
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNew(string cs, TblMember member)
        {
            try
            {
                TblMember _member = GetMemberByID(cs, member.MemberId);
                using (var db = new SaleManagementContext(cs))
                {
                    if (_member == null)
                    {
                        db.TblMembers.Add(member);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The member ia already exists");
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(string cs, int memberId)
        {
            try
            {
                TblMember _member = GetMemberByID(cs, memberId);
                using (var db = new SaleManagementContext(cs))
                {
                    if (_member != null)
                    {
                        db.TblMembers.Remove(_member);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The member does not already exists");
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

