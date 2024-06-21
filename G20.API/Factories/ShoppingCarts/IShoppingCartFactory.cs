using G20.API.Models.ShoppingCarts;

namespace G20.API.Factories.ShoppingCarts
{
    public interface IShoppingCartFactory
    {
        Task<ShoppingCartModel> GetShoppingCartDetailByUserId(int userId);
    }
}
