using Microsoft.AspNetCore.Mvc;
using G20.API.Models.Coupons;
using G20.Core.Domain;
using G20.Core;
using G20.Service.Coupons;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Factories.Coupons;

namespace G20.API.Controllers
{
    public class CouponController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly ICouponFactoryModel _couponFactoryModel;
        protected readonly ICouponService _couponService;

        public CouponController(IWorkContext workContext,
            ICouponFactoryModel couponFactoryModel,
            ICouponService couponService)
        {
            _workContext = workContext;
            _couponFactoryModel = couponFactoryModel;
            _couponService = couponService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(CouponSearchModel searchModel)
        {
            var model = await _couponFactoryModel.PrepareCouponListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var coupon = await _couponService.GetByIdAsync(id);
            if (coupon == null)
                return Error("not found");
            return Success(coupon.ToModel<CouponModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(CouponModel model)
        {
            var coupon = model.ToEntity<Coupon>();
            await _couponService.InsertAsync(coupon);
            return Success(coupon.ToModel<CouponModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(CouponModel model)
        {
            var coupon = await _couponService.GetByIdAsync(model.Id);
            if (coupon == null)
                return Error("not found");

            coupon = model.ToEntity(coupon);
            await _couponService.UpdateAsync(coupon);
            return Success(coupon.ToModel<CouponModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var coupon = await _couponService.GetByIdAsync(id);
            if (coupon == null)
                return Error("not found");
            await _couponService.DeleteAsync(coupon);
            return Success(id);
        }
    }
}
