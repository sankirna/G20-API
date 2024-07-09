using G20.API.Factories.Coupons;
using G20.API.Factories.Media;
using G20.API.Factories.Products;
using G20.API.Factories.Users;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Coupons;
using G20.API.Models.Orders;
using G20.API.Models.ShoppingCarts;
using G20.API.Models.Users;
using G20.Core.Domain;
using G20.Core.Enums;
using G20.Service.BoardingDetails;
using G20.Service.Coupons;
using G20.Service.Orders;
using G20.Service.Products;
using G20.Service.ProductTicketCategoriesMap;
using G20.Service.TicketCategories;
using G20.Service.Users;
using Nop.Core;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Factories.Orders
{
    public class OrderFactory : IOrderFactory
    {
        protected readonly IOrderService _orderService;
        protected readonly IUserService _userservice;
        protected readonly IUserFactoryModel _userFactoryModel;
        protected readonly IMediaFactoryModel _mediaFactoryModel;
        protected readonly ICouponService _couponService;
        protected readonly ICouponFactoryModel _couponFactoryModel;
        protected readonly IProductService _productService;
        protected readonly IProductFactoryModel _productFactoryModel;
        protected readonly IOrderProductItemService _orderProductItemService;
        protected readonly IOrderProductItemDetailService _orderProductItemDetailService;
        protected readonly IProductTicketCategoryMapService _productTicketCategoryMapService;
        protected readonly ITicketCategoryService _ticketCategoryService;
        protected readonly IBoardingDetailService _boardingDetailService;

        public OrderFactory(
              IOrderService orderService
            , IUserService userService
            , IUserFactoryModel userFactoryModel
            , IMediaFactoryModel mediaFactoryModel
            , ICouponService couponService
            , ICouponFactoryModel couponFactoryModel
            , IProductService productService
            , IProductFactoryModel productFactoryModel
            , IOrderProductItemService orderProductItemService
            , IOrderProductItemDetailService orderProductItemDetailService
            , IProductTicketCategoryMapService productTicketCategoryMapService
            , ITicketCategoryService ticketCategoryService
            , IBoardingDetailService boardingDetailService)
        {
            _orderService = orderService;
            _userservice = userService;
            _userFactoryModel = userFactoryModel;
            _mediaFactoryModel = mediaFactoryModel;
            _couponService = couponService;
            _couponFactoryModel = couponFactoryModel;
            _productService = productService;
            _productFactoryModel = productFactoryModel;
            _orderProductItemService = orderProductItemService;
            _orderProductItemDetailService = orderProductItemDetailService;
            _productTicketCategoryMapService = productTicketCategoryMapService;
            _ticketCategoryService = ticketCategoryService;
            _boardingDetailService = boardingDetailService;
        }

        public virtual async Task<OrderListModel> PrepareOrderListModelAsync(OrderListRequestModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var orders = await _orderService.GetOrdersAsync(
                userId: searchModel.userId,
                orderStatusEnum: searchModel.OrderStatusEnum,
                fromDate: searchModel.FromDate,
                toDate: searchModel.ToDate,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new OrderListModel().PrepareToGridAsync(searchModel, orders, () =>
            {
                return orders.SelectAwait(async order =>
                {
                    var orderDetailModel = await GetOrderDetailModelAsync(order
                        , isUserDetail: true
                        , isCouponDetail: true);
                    return orderDetailModel;
                });
            });

            return model;
        }

        public virtual async Task<OrderListModel> PrepareOrderListModelAsync(UserOrderHistoryRequestModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var orders = await _orderService.GetOrdersAsync(
                userId: searchModel.UserId,
                orderStatusEnum: searchModel.OrderStatusEnum,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new OrderListModel().PrepareToGridAsync(searchModel, orders, () =>
            {
                return orders.SelectAwait(async order =>
                {
                    var orderDetailModel = await GetOrderDetailModelAsync(order
                                                                    , isUserDetail: true
                                                                    , isCouponDetail: true
                                                                    , isOrderProductItem: true
                                                                    , isProductTicketCategoryMapDetail: true);
                    return orderDetailModel;
                });
            });

            return model;
        }

        public virtual async Task<OrderDetailModel> GetOrderDetailModelAsync(Order order
             , bool isUserDetail = false
             , bool isCouponDetail = false
             , bool isOrderProductItem = false
             , bool isOrderProductItemDetail = false
             , bool isProductTicketCategoryMapDetail = false)
        {
            var orderDetailModel = order.ToModel<OrderDetailModel>();

            var user = await _userservice.GetByIdAsync(orderDetailModel.UserId);
            if (isUserDetail && user != null)
            {
                orderDetailModel.UserDetail = await _userFactoryModel.PrepareUserModelAsync(user);
            }

            if (isCouponDetail)
            {
                if (orderDetailModel.CouponCode != null)
                {
                    var coupon = await _couponService.GetByCodeAsync(orderDetailModel.CouponCode);
                    orderDetailModel.CouponDetail = await _couponFactoryModel.PrepareCouponModelAsync(coupon);
                }
            }
            if (isOrderProductItem)
            {
                var orderProductItems = (await _orderProductItemService.GetOrderProductItemsAsync(orderDetailModel.Id)).ToList();
                orderDetailModel.Items = new List<OrderProductItemModel>();
                if (orderProductItems != null && orderProductItems.Any())
                {
                    foreach (var item in orderProductItems)
                    {
                        var orderProductItemModel = await GetOrderProductItemModelAsync(item
                                                                  , isProductDetail: true
                                                                  , isOrderProductItemDetail:true);
                        orderDetailModel.Items.Add(orderProductItemModel);
                    }
                }
            }

            return orderDetailModel;
        }

        public virtual async Task<OrderProductItemModel> GetOrderProductItemModelAsync(OrderProductItem orderProductItem
            , bool isProductDetail = false
            , bool isOrderProductItemDetail = false)
        {
            var orderProductItemModel = orderProductItem.ToModel<OrderProductItemModel>();
            if (isProductDetail)
            {
                var product = await _productService.GetByIdAsync(orderProductItemModel.ProductId);
                orderProductItemModel.ProductDetail = await _productFactoryModel.PrepareProductDetailModelAsync(product
                                                                , isCategoryDetail: true
                                                                , isVenueDetail: true
                                                                , isTeam1Detail: true
                                                                , isTeam2Detail: true);
            }
            if (isOrderProductItemDetail)
            {
                var orderProductItemDetails = await _orderProductItemDetailService.GetDetailsByOrderProductItemIdAsync(orderProductItem.Id);
                orderProductItemModel.OrderProductItemDetail = await GetOrderProductItemDetailModelAsync(orderProductItemDetails, isQRCodeFile: true);
            }
            return orderProductItemModel;
        }

        public virtual async Task<OrderProductItemDetailModel> GetOrderProductItemDetailModelAsync(OrderProductItemDetail orderProductItemDetail
            , bool isQRCodeFile = false)
        {
            var orderProductItemModel = orderProductItemDetail.ToModel<OrderProductItemDetailModel>();
            if (isQRCodeFile)
            {
                orderProductItemModel.QRCodeFile = await _mediaFactoryModel.GetRequestModelAsync(orderProductItemModel.QRCodeFileId);
            }
            return orderProductItemModel;
        }


        public virtual OrderModel MapOrderModelFromShoppingModel(ShoppingCartModel model)
        {
            var orderModel = model.ToModel<OrderModel>();
            orderModel.Items = MapOrderProductItemModelFromShoppingItemModel(model.Items);
            return orderModel;
        }

        public virtual List<OrderProductItemModel> MapOrderProductItemModelFromShoppingItemModel(List<ShoppingCartItemModel> shoppingCartItems)
        {
            var orderProductItems = shoppingCartItems.Select(x => x.ToModel<OrderProductItemModel>()).ToList();
            return orderProductItems;
        }

        public virtual async Task<OrderCouponInfoModel> GetAndValidateCouponInfoByCode(OrderModel orderModel)
        {
            if (string.IsNullOrEmpty(orderModel.CouponCode))
                return null;

            var coupon = await _couponService.GetByCodeAsync(orderModel.CouponCode);
            if (coupon == null)
                throw new NopException("Invalid coupon");

            OrderCouponInfoModel orderCouponInfoModel = new OrderCouponInfoModel();
            orderCouponInfoModel.CouponId = coupon.Id;
            if (coupon.IsQuantity)
            {
                if (coupon.MinimumQuantity <= orderModel.TotalQuantity)
                {
                    throw new NopException("Please add {0} tickets", (coupon.MinimumQuantity - orderModel.TotalQuantity));
                }
            }
            switch ((CouponCalculateType)coupon.TypeId)
            {
                case CouponCalculateType.Amount:
                    orderCouponInfoModel.Discount = coupon.Amount;
                    break;
                case CouponCalculateType.Percentage:
                    orderCouponInfoModel.Discount = orderModel.GrossTotal / coupon.Amount;
                    break;
                default:
                    break;
            }
            return orderCouponInfoModel;
        }

        public virtual async Task<IList<Product>> GetAndValidateProductDetails(List<OrderProductItemModel> items)
        {

            var productIds = items.Select(x => x.ProductId).ToList();
            var productTicketCategoryMapIds = items.Select(x => x.ProductTicketCategoryMapId).ToList();
            var products = await _productService.GetByProductIdsAsync(productIds);
            var productTicketCategoryMaps = await _productTicketCategoryMapService.GetProductTicketCategoryMapsByIdsAsync(productTicketCategoryMapIds);

            if (productIds.DistinctBy(x => x).Count() != products.Count)
                throw new NopException("Some of product(s) are invalid");

            if (productTicketCategoryMapIds.Count != productTicketCategoryMaps.Count)
                throw new NopException("Some of product ticket category(s) are invalid");

            //Check valid product and product ticket categories
            foreach (var item in items)
            {
                var productTicketCategoryMap = productTicketCategoryMaps.FirstOrDefault(x => x.Id == item.ProductTicketCategoryMapId);
                if (productTicketCategoryMap != null
                     && productTicketCategoryMap.ProductId != item.ProductId)
                {
                    throw new NopException("Product and product ticket category(s) is invalid");
                }
            }

            foreach (var product in products)
            {
                product.ProductTicketCategoryMaps = productTicketCategoryMaps.Where(x => x.ProductId == product.Id).ToList();
            }

            return products;
        }

        public virtual async Task<bool> CheckProductTicketAvaibility(IList<Product> productDetails, List<OrderProductItemModel> orderProductRequestItems)
        {
            foreach (var orderProductRequestItem in orderProductRequestItems)
            {
                var product = productDetails.FirstOrDefault(x => x.Id == orderProductRequestItem.ProductId);
                var productTicketCategoryMap = product.ProductTicketCategoryMaps.FirstOrDefault(x => x.Id == orderProductRequestItem.ProductTicketCategoryMapId);
                orderProductRequestItem.Price = productTicketCategoryMap.Price;
                orderProductRequestItem.IsOutofStock = productTicketCategoryMap.IsOutOfStock(orderProductRequestItem.Quantity);
            }
            return orderProductRequestItems.Any(x => !x.IsOutofStock);
        }

        public virtual async Task<UserProductItemDetail> GetOrderProductItemDetailModelAsync(OrderProductItemDetail orderProductItemDetail)
        {
            var orderProductItem = await _orderProductItemService.GetByIdAsync(orderProductItemDetail.OrderProductItemId);
            UserProductItemDetail userProductDetail = new UserProductItemDetail();
            userProductDetail.OrderProductItemDetailId = orderProductItemDetail.OrderProductItemId;
            userProductDetail.TotalQuantity = orderProductItem.Quantity;
            userProductDetail.RemainingQuantity = _boardingDetailService.GetBoardingQuanity(orderProductItemDetail.Id, orderProductItem.Quantity);

            userProductDetail.UserId = orderProductItemDetail.UserId;
            //userProductDetail.OrderProductItemDetail = orderProductItemDetail;
            //userProductDetail.OrderProductItemModel = orderProductItem;
            orderProductItem.ProductTicketCategoryMap = await _productTicketCategoryMapService.GetByIdAsync(orderProductItem.ProductTicketCategoryMapId);
            var user = await _userservice.GetByIdAsync(orderProductItem.UserId);
            if (user != null)
            {
                userProductDetail.UserName = user.UserName;
            }
            userProductDetail.StandName = _ticketCategoryService.GetByIdAsync(orderProductItem.ProductTicketCategoryMap.TicketCategoryId).Result.Name;
            return userProductDetail;
        }

    }
}
