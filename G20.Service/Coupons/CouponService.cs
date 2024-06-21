using G20.Core.Domain;
using G20.Core;
using G20.Data;

namespace G20.Service.Coupons
{
    public class CouponService : ICouponService
    {
        protected readonly IRepository<Coupon> _entityRepository;

        public CouponService(IRepository<Coupon> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<Coupon>> GetCouponsAsync(string code, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var coupons = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(code))
                    query = query.Where(c => c.Code.Contains(code));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return coupons;
        }

        public virtual async Task<Coupon> GetByIdAsync(int Id)
        {
            return await _entityRepository.GetByIdAsync(Id);
        }

        public virtual async Task<Coupon> GetByCodeAsync(string couponCode)
        {
            return await _entityRepository.Table.FirstOrDefaultAsync(x => !x.IsDeleted && x.Code == couponCode.Trim());
        }

        public virtual async Task InsertAsync(Coupon entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(Coupon entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Coupon entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _entityRepository.DeleteAsync(entity);
        }
    }
}
