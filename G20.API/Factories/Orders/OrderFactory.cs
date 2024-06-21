using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Coupons;
using G20.API.Models.Orders;
using G20.API.Models.ShoppingCarts;
using G20.Core.Domain;
using G20.Core.Enums;
using G20.Service.Coupons;
using G20.Service.Products;
using G20.Service.ProductTicketCategoriesMap;
using Nop.Core;

namespace G20.API.Factories.Orders
{
    public class OrderFactory
    {
        protected readonly ICouponService _couponService;
        protected readonly IProductService _productService;
        protected readonly IProductTicketCategoryMapService _productTicketCategoryMapService;

        public OrderFactory(ICouponService couponService
            , IProductService productService
            , IProductTicketCategoryMapService productTicketCategoryMapService)
        {
            _couponService = couponService;
            _productService = productService;
            _productTicketCategoryMapService = productTicketCategoryMapService;
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

        public virtual async Task<OrderCouponInfoModel> GetCouponInfoByCode(OrderModel orderModel)
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

        public virtual async Task<IList<Product>> GetProductDetails(List<OrderProductItemModel> items)
        {

            var productIds = items.Select(x => x.ProductId).ToList();
            var productTicketCategoryMapIds = items.Select(x => x.ProductTicketCategoryMapId).ToList();
            var products = await _productService.GetByProductIdsAsync(productIds);
            var productTicketCategoryMaps = await _productTicketCategoryMapService.GetProductTicketCategoryMapsByIdsAsync(productTicketCategoryMapIds);

            if (productIds.DistinctBy(x => x).Count() != products.Count)
                throw new NopException("Some of product(s) are invalid");

            if (productTicketCategoryMapIds.Count != productTicketCategoryMaps.Count)
                throw new NopException("Some of product ticket category(s) are invalid");

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
                orderProductRequestItem.IsOutofStock = productTicketCategoryMap.IsOutOfStock(orderProductRequestItem.Quantity);
            }
            return orderProductRequestItems.Any(x => !x.IsOutofStock);
        }
    }
}
