using G20.Core.Enums;
using G20.Framework.Models;

namespace G20.API.Models.Coupons
{
    public partial record CouponModel : BaseNopEntityModel
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public int TypeId { get; set; }
        public CouponCalculateType Type { get; set; }
        public string TypeValue { get { return Type.ToString(); } }
        public DateTime? ExpirationDate { get; set; }

    }
}
