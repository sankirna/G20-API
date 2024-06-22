﻿using G20.Core;
using G20.Core.Domain;


namespace G20.Service.Orders
{
    public interface IOrderService
    {
        Task<IPagedList<Order>> GetOrdersAsync(int userId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Order> GetByIdAsync(int id);
        Task InsertAsync(Order entity);
        Task UpdateAsync(Order entity);
        Task DeleteAsync(Order entity);
        Task<bool> SendOrderNotifications(int orderId);
    }
}
