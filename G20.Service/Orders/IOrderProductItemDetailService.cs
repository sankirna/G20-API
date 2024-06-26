using G20.Core.Domain;
using G20.Core;


namespace G20.Service.Orders
{
    public interface IOrderProductItemDetailService
    {
        Task<IPagedList<OrderProductItemDetail>> GetOrderProductItemDetailsAsync(int orderProductItemId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<IList<OrderProductItemDetail>> GetOrderProductItemDetailsByOrderProductItemIdAsync(int orderProductItemId);
        Task<OrderProductItemDetail> GetByIdAsync(int id);
        Task InsertAsync(OrderProductItemDetail entity);
        Task UpdateAsync(OrderProductItemDetail entity);
        Task DeleteAsync(OrderProductItemDetail entity);
        Task<OrderProductItemDetail> GetOrderProductItemDetailsByQRCodeAsync(int ProductId, string QRCode);
    }
}
