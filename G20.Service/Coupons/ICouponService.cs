using G20.Core.Domain;
using G20.Core;

namespace G20.Service.Coupons
{
    public interface ICouponService
    {
        Task<IPagedList<Coupon>> GetCouponsAsync(string code, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Coupon> GetByIdAsync(int Id);
        Task<Coupon> GetByCodeAsync(string couponCode);
        Task InsertAsync(Coupon entity);
        Task UpdateAsync(Coupon entity);
        Task DeleteAsync(Coupon entity);
    }
}
