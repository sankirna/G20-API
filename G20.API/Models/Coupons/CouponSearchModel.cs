using Nop.Web.Framework.Models;

namespace G20.API.Models.Coupons
{
    public partial record CouponSearchModel : BaseSearchModel
    {
        public string Code { get; set; }
    }
}
