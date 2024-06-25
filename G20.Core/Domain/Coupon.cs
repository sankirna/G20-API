using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Core.Domain
{
    public partial class Coupon : BaseEntityWithTacking
    {
        public string Code { get; set; }
        public bool IsQuantity { get; set; }
        public int? MinimumQuantity { get; set; }
        public decimal Amount { get; set; }
        public int TypeId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
