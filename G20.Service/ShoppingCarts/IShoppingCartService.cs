using G20.Core.Domain;
using G20.Core;

namespace G20.Service.ShoppingCarts
{
    public interface IShoppingCartService
    {
        Task<IPagedList<ShoppingCart>> GetShoppingCartsAsync(int userId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<ShoppingCart> GetByIdAsync(int id);
         Task<ShoppingCart> GetByUserIdAsync(int id);
        Task InsertAsync(ShoppingCart entity);
        Task UpdateAsync(ShoppingCart entity);
        Task DeleteAsync(ShoppingCart entity);
    }
}
