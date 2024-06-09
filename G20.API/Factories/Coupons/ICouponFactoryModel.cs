using G20.API.Models.Coupons;

namespace G20.API.Factories.Coupons
{
    public interface ICouponFactoryModel
    {
        Task<CouponListModel> PrepareCouponListModelAsync(CouponSearchModel searchModel);
    }
}
