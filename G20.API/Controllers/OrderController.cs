

using G20.API.Factories.Media;
using G20.API.Factories.Orders;
using G20.API.Factories.ShoppingCarts;
using G20.API.Models.Checkout;
using G20.API.Models.Orders;
using G20.API.Models.Venue;
using G20.Core;
using G20.Core.Domain;
using G20.Service.Orders;
using G20.Service.ProductCombos;
using G20.Service.Products;
using G20.Service.ProductTicketCategoriesMap;
using G20.Service.QRCodes;
using G20.Service.ShoppingCarts;
using Microsoft.AspNetCore.Mvc;

namespace G20.API.Controllers
{
    public class OrderController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IShoppingCartService _shoppingCartService;
        protected readonly IShoppingCartItemService _shoppingCartItemService;
        protected readonly IShoppingCartFactory _shoppingCartFactory;
        protected readonly IOrderFactory _orderFactory;
        protected readonly IMediaFactoryModel _mediaFactoryModel;
        protected readonly IProductService _productService;
        protected readonly IProductComboService _productComboService;
        protected readonly IProductTicketCategoryMapService _productTicketCategoryMapService;
        protected readonly IOrderService _orderService;
        protected readonly IOrderProductItemService _orderProductItemService;
        protected readonly IOrderProductItemDetailService _orderProductItemDetailService;
        protected readonly IQRCodeService _qrCodeService;

        public OrderController(IWorkContext workContext
            , IShoppingCartService shoppingCartService
            , IShoppingCartItemService shoppingCartItemService
            , IShoppingCartFactory shoppingCartFactory
            , IMediaFactoryModel mediaFactoryModel
            , IProductService productService
            , IProductComboService productComboService
            , IProductTicketCategoryMapService productTicketCategoryMapService
            , IOrderFactory orderFactory
            , IOrderService orderService
            , IOrderProductItemService orderProductItemService
            , IOrderProductItemDetailService orderProductItemDetailService
            , IQRCodeService qrCodeService)
        {
            _workContext = workContext;
            _shoppingCartService = shoppingCartService;
            _shoppingCartItemService = shoppingCartItemService;
            _productService = productService;
            _productComboService = productComboService;
            _productTicketCategoryMapService = productTicketCategoryMapService;
            _shoppingCartFactory = shoppingCartFactory;
            _orderFactory = orderFactory;
            _mediaFactoryModel = mediaFactoryModel;
            _orderService = orderService;
            _orderProductItemService = orderProductItemService;
            _orderProductItemDetailService = orderProductItemDetailService;
            _qrCodeService = qrCodeService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> GetOrders(OrderListRequestModel searchModel)
        {
            var userId = _workContext.GetCurrentUserId();
            var model = await _orderFactory.PrepareOrderListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
                return Error("not found");
            var model = await _orderFactory.GetOrderDetailModelAsync(order
                                                                    , isUserDetail: true
                                                                    , isCouponDetail: true
                                                                    , isOrderProductItem: true
                                                                    , isProductTicketCategoryMapDetail: true);
            return Success(model);
        }
    }
}
