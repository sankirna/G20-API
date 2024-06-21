using G20.Core.Domain;
using G20.Core;

namespace G20.Service.ShoppingCarts
{
    public interface IShoppingCartItemService
    {
        Task<IPagedList<ShoppingCartItem>> GetShoppingCartItemsAsync(int shoppingCartId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<ShoppingCartItem> GetByIdAsync(int id);
        Task<IList<ShoppingCartItem>> GetByShoppingCartIdAsync(int shoppingCartId);
        Task InsertAsync(ShoppingCartItem entity);
        Task UpdateAsync(ShoppingCartItem entity);
        Task DeleteAsync(ShoppingCartItem entity);
    }
}
