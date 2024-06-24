using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Coupons;
using G20.API.Models.Orders;
using G20.API.Models.ShoppingCarts;
using G20.Core.Domain;
using G20.Core.Enums;
using G20.Service.Coupons;
using G20.Service.Orders;
using G20.Service.Products;
using G20.Service.ProductTicketCategoriesMap;
using G20.Service.Users;
using Nop.Core;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Factories.Orders
{
    public class OrderFactory : IOrderFactory
    {
        protected readonly IOrderService _orderService;
        protected readonly IUserService _userservice;
        protected readonly ICouponService _couponService;
        protected readonly IProductService _productService;
        protected readonly IProductTicketCategoryMapService _productTicketCategoryMapService;

        public OrderFactory(
              IOrderService orderService,
              IUserService userService
            , ICouponService couponService
            , IProductService productService
            , IProductTicketCategoryMapService productTicketCategoryMapService)
        {
            _orderService = orderService;
            _userservice = userService;
            _couponService = couponService;
            _productService = productService;
            _productTicketCategoryMapService = productTicketCategoryMapService;
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
                    var orderDetailModel = await GetOrderDetailModelAsync(order);
                    
                    return orderDetailModel;
                });
            });

            return model;
        }

        public virtual async Task<OrderDetailModel> GetOrderDetailModelAsync(Order order)
        {
            var orderDetailModel = order.ToModel<OrderDetailModel>();
            //User Name
            var user = await _userservice.GetByIdAsync(orderDetailModel.UserId);
            if (user != null)
            {
                orderDetailModel.UserName = user.UserName;
            }
            return orderDetailModel;
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
    }
}
