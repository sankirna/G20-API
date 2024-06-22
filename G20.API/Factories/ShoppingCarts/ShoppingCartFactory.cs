using G20.API.Factories.Orders;
using G20.API.Factories.Products;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Orders;
using G20.API.Models.ShoppingCarts;
using G20.Core;
using G20.Core.Domain;
using G20.Service.ShoppingCarts;

namespace G20.API.Factories.ShoppingCarts
{
    public class ShoppingCartFactory : IShoppingCartFactory
    {
        protected readonly IWorkContext _workContext;
        protected readonly IShoppingCartService _shoppingCartService;
        protected readonly IShoppingCartItemService _shoppingCartItemService;
        protected readonly IProductFactoryModel _productFactoryModel;
        protected readonly IOrderFactory _orderFactory;

        public ShoppingCartFactory(IWorkContext workContext
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

        public virtual ShoppingCartModel MapShoppingModelFromOrderModel(OrderModel model)
        {
            var shoppingCartModel = model.ToModel<ShoppingCartModel>();
            shoppingCartModel.Items = MapShoppingItemModelFromOrderProductItemModel(model.Items);
            return shoppingCartModel;
        }

        public virtual List<ShoppingCartItemModel> MapShoppingItemModelFromOrderProductItemModel(List<OrderProductItemModel> orderProductItemModels)
        {
            var shoppingCartItems = orderProductItemModels.Select(x => x.ToModel<ShoppingCartItemModel>()).ToList();
            return shoppingCartItems;
        }

        public virtual async Task<ShoppingCartModel> GetShoppingCartDetailByUserId(int userId)
        {
            var shoppingCart = await _shoppingCartService.GetByUserIdAsync(userId);
            if (shoppingCart == null)
                return null;

            var shoppingCartModel = shoppingCart.ToModel<ShoppingCartModel>();

            shoppingCartModel.Items = await GetShoppingCartItemDetailsByShoppingCartId(shoppingCart.Id);
            return shoppingCartModel;
        }

        public virtual async Task<List<ShoppingCartItemModel>> GetShoppingCartItemDetailsByShoppingCartId(int shoppingCartId)
        {
            var shoppingCartItems = await _shoppingCartItemService.GetByShoppingCartIdAsync(shoppingCartId);
            List<ShoppingCartItemModel> shoppingCartItemModels = new List<ShoppingCartItemModel>();
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                ShoppingCartItemModel shoppingCartItemModel = shoppingCartItem.ToModel<ShoppingCartItemModel>();
                shoppingCartItemModel.ProductDetail = await _productFactoryModel.PrepareProductModelAsync(shoppingCartItemModel.ProductId);
                shoppingCartItemModel.ProductTicketCategoryMapDetail = await _productFactoryModel.PrepareProductTicketCategoryMapModelAsync(shoppingCartItemModel.ProductTicketCategoryMapId);
                shoppingCartItemModels.Add(shoppingCartItemModel);
            }
            return shoppingCartItemModels;
        }
    }
}
