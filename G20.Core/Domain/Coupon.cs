using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Core.Domain
{
    public partial class Coupon : BaseEntity
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public int TypeId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public virtual AspNetUser? CreatedByNavigation { get; set; }
        public virtual AspNetUser? UpdatedByNavigation { get; set; }

    }
}
