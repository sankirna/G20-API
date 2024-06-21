using G20.Core.Domain;
using G20.Core;
using G20.Data;

namespace G20.Service.Orders
{
    public class OrderProductItemDetailService : IOrderProductItemDetailService
    {
        protected readonly IRepository<OrderProductItemDetail> _entityRepository;

        public OrderProductItemDetailService(IRepository<OrderProductItemDetail> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<OrderProductItemDetail>> GetOrderProductItemDetailsAsync(int orderProductItemId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var orderProductItemDetails = await _entityRepository.GetAllPagedAsync(query =>
            {
                query = query.Where(c => c.OrderProductItemId == orderProductItemId);
                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return orderProductItemDetails;
        }

        public virtual async Task<OrderProductItemDetail> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(OrderProductItemDetail entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(OrderProductItemDetail entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(OrderProductItemDetail entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
