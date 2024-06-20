using G20.Core.Domain;
using G20.Core;


namespace G20.Service.Orders
{
    public interface IOrderProductItemService
    {
        Task<IPagedList<OrderProductItem>> GetOrderProductItemsAsync(int orderId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<OrderProductItem> GetByIdAsync(int id);
        Task InsertAsync(OrderProductItem entity);
        Task UpdateAsync(OrderProductItem entity);
        Task DeleteAsync(OrderProductItem entity);
    }
}
