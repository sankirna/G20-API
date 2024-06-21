using G20.API.Models.Orders;
using G20.API.Models.ShoppingCarts;

namespace G20.API.Factories.ShoppingCarts
{
    public interface IShoppingCartFactory
    {
        ShoppingCartModel MapShoppingModelFromOrderModel(OrderModel model);
        List<ShoppingCartItemModel> MapShoppingItemModelFromOrderProductItemModel(List<OrderProductItemModel> orderProductItemModels);
        Task<ShoppingCartModel> GetShoppingCartDetailByUserId(int userId);
        Task<List<ShoppingCartItemModel>> GetShoppingCartItemDetailsByShoppingCartId(int shoppingCartId);
    }
}
