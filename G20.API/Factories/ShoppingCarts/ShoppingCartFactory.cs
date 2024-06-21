using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.ShoppingCarts;
using G20.Core;
using G20.Service.ShoppingCarts;

namespace G20.API.Factories.ShoppingCarts
{
    public class ShoppingCartFactory : IShoppingCartFactory
    {
        protected readonly IWorkContext _workContext;
        protected readonly IShoppingCartService _shoppingCartService;
        protected readonly IShoppingCartItemService _shoppingCartItemService;


        public ShoppingCartFactory(IWorkContext workContext
            , IShoppingCartService shoppingCartService
            , IShoppingCartItemService shoppingCartItemService)
        {
            _workContext = workContext;
            _shoppingCartService = shoppingCartService;
            _shoppingCartItemService = shoppingCartItemService;
        }

        public virtual async Task<ShoppingCartModel> GetShoppingCartDetailByUserId(int userId)
        {
            var shoppingCart = await _shoppingCartService.GetByUserIdAsync(userId);
            if (shoppingCart == null)
                return null;

            var shoppingCartModel = shoppingCart.ToModel<ShoppingCartModel>();

            var shoppingCartItems = await _shoppingCartItemService.GetByShoppingCartIdAsync(shoppingCart.Id);
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                ShoppingCartItemModel shoppingCartItemModel = shoppingCartItem.ToModel<ShoppingCartItemModel>();
                shoppingCartModel.Items.Add(shoppingCartItemModel);
            }
            return shoppingCartModel;
        }

    }
}
