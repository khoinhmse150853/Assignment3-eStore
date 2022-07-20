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

        public List<TblMember> GetMemberList()
        {
            using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
            {
                return db.TblMembers.ToList();
            }
        }

        public List<TblMember> GetMemberListByUser(int memberId)
        {
            using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
            {
                return db.TblMembers.Where(X => X.MemberId == memberId).ToList();
            }
        }

        public TblMember GetMemberByID(int memberId)
        {
            TblMember member = null;
            try
            {
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
                {
                    member = db.TblMembers.SingleOrDefault(p => p.MemberId == memberId);
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        public void Update(TblMember member)
        {
            try
            {
                TblMember _member = GetMemberByID(member.MemberId);
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
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

        public void AddNew(TblMember member)
        {
            try
            {
                TblMember _member = GetMemberByID(member.MemberId);
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
                {
                    if (_member == null)
                    {
                        db.TblMembers.Add(member);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The member is already exists");
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int memberId)
        {
            try
            {
                TblMember _member = GetMemberByID(memberId);
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
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
