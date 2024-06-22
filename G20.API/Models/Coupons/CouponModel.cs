using G20.Core.Enums;
using G20.Framework.Models;

namespace G20.API.Models.Coupons
{
    public partial record CouponModel : BaseNopEntityModel
    {
        public string Code { get; set; }
        public Boolean? IsQuantity { get; set; }
        public int? MinimumQuantity { get; set; }
        public decimal Amount { get; set; }
        public CouponCalculateType TypeId { get; set; }
        public string TypeValue { get { return TypeId.ToString(); } }
        public DateTime? ExpirationDate { get; set; }

    }
}
