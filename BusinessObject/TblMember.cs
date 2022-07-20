using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class TblMember
    {
        public TblMember()
        {
            TblOrders = new HashSet<TblOrder>();
        }
        public TblMember(string email, string memberPassword)
        {
            Email = email;
            MemberPassword = memberPassword;
        }
        public int MemberId { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string MemberPassword { get; set; }

        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
