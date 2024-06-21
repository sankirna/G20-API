﻿using G20.API.Models.Orders;
using G20.API.Models.ShoppingCarts;
using G20.Core.Domain;

namespace G20.API.Factories.Orders
{
    public interface IOrderFactory
    {
        OrderModel MapOrderModelFromShoppingModel(ShoppingCartModel model);
        List<OrderProductItemModel> MapOrderProductItemModelFromShoppingItemModel(List<ShoppingCartItemModel> shoppingCartItems);
        Task<OrderCouponInfoModel> GetAndValidateCouponInfoByCode(OrderModel orderModel);
        Task<IList<Product>> GetAndValidateProductDetails(List<OrderProductItemModel> items);

        Task<bool> CheckProductTicketAvaibility(IList<Product> productDetails, List<OrderProductItemModel> orderProductRequestItems);
    }
}
