using G20.Core.Domain;
using G20.Core;
using G20.Data;

namespace G20.Service.Orders
{
    public class OrderProductItemService : IOrderProductItemService
    {
        protected readonly IRepository<OrderProductItem> _entityRepository;

        public OrderProductItemService(IRepository<OrderProductItem> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<OrderProductItem>> GetOrderProductItemsAsync(int orderId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var orderProductItems = await _entityRepository.GetAllPagedAsync(query =>
            {
                query = query.Where(c => c.OrderId == orderId);
                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return orderProductItems;
        }

        public virtual async Task<OrderProductItem> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(OrderProductItem entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(OrderProductItem entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(OrderProductItem entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
