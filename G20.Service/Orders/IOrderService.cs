using G20.Core;
using G20.Core.Domain;
using G20.Core.Enums;


namespace G20.Service.Orders
{
    public interface IOrderService
    {
        Task<IPagedList<Order>> GetOrdersAsync(int userId = 0,
                                                OrderStatusEnum? orderStatusEnum = null,
                                                DateTime? fromDate = null,
                                                DateTime? toDate = null,
                                                int pageIndex = 0,
                                                int pageSize = int.MaxValue,
                                                bool getOnlyTotalCount = false);
        Task<Order> GetByIdAsync(int id);
        Task InsertAsync(Order entity);
        Task UpdateAsync(Order entity);
        Task UpdateOrderStatus(Order entity, OrderStatusEnum orderStatusEnum);
        Task DeleteAsync(Order entity);
        Task<bool> SendOrderNotifications(int orderId);
    }
}
