using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject
{
    public partial class TblOrder
    {
        public TblOrder()
        {
            TblOrderDetails = new HashSet<TblOrderDetail>();
        }

        public int OrderId { get; set; }
        public int MemberId { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? RequiredDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }

        public virtual TblMember Member { get; set; }
        public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; }
    }
}
