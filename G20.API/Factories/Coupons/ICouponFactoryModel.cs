using G20.API.Models.Coupons;
using G20.Core.Domain;

namespace G20.API.Factories.Coupons
{
    public interface ICouponFactoryModel
    {
        Task<CouponListModel> PrepareCouponListModelAsync(CouponSearchModel searchModel);
        Task<CouponModel> PrepareCouponModelAsync(Coupon entity, bool isDetail = false);
    }
}
