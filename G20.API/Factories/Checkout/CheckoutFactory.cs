using G20.API.Factories.Orders;
using G20.API.Factories.Products;
using G20.API.Models.ShoppingCarts;
using G20.Core;
using G20.Core.Domain;
using G20.Service.ShoppingCarts;

namespace G20.API.Factories.Checkout
{
    public class CheckoutFactory
    {
        protected readonly IWorkContext _workContext;
        protected readonly IShoppingCartService _shoppingCartService;
        protected readonly IShoppingCartItemService _shoppingCartItemService;
        protected readonly IProductFactoryModel _productFactoryModel;
        protected readonly IOrderFactory _orderFactory;

        public CheckoutFactory(IWorkContext workContext
            , IShoppingCartService shoppingCartService
            , IShoppingCartItemService shoppingCartItemService
            , IProductFactoryModel productFactoryModel
            , IOrderFactory orderFactory)
        {
            _workContext = workContext;
            _shoppingCartService = shoppingCartService;
            _shoppingCartItemService = shoppingCartItemService;
            _productFactoryModel = productFactoryModel;
            _orderFactory = orderFactory;
        }

        public virtual async Task<Order> MapOrderFromShoppingCart(ShoppingCartModel shoppingCartModel)
        {
            return null;
        } 

    }
}
