using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Coupons;
using G20.Service.Coupons;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Factories.Coupons
{
    public class CouponFactoryModel : ICouponFactoryModel
    {
        protected readonly ICouponService _couponService;

        public CouponFactoryModel(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public virtual async Task<CouponListModel> PrepareCouponListModelAsync(CouponSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var coupons = await _couponService.GetCouponsAsync(code: searchModel.Code,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new CouponListModel().PrepareToGridAsync(searchModel, coupons, () =>
            {
                return coupons.SelectAwait(async coupon =>
                {
                    var couponModel = coupon.ToModel<CouponModel>();
                    return couponModel;
                });
            });

            return model;
        }
    }
}
