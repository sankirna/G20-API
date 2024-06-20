using G20.Core;
using G20.Core.Domain;
using G20.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Orders
{
    public class OrderService : IOrderService
    {
        protected readonly IRepository<Order> _entityRepository;

        public OrderService(IRepository<Order> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<Order>> GetOrdersAsync(int userId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var orders = await _entityRepository.GetAllPagedAsync(query =>
            {
                query = query.Where(o => o.UserId == userId);
                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return orders;
        }

        public virtual async Task<Order> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(Order entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(Order entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Order entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
