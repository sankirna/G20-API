using G20.API.Models.Orders;
using G20.API.Models.ShoppingCarts;
using G20.Core.Domain;

namespace G20.API.Factories.Orders
{
    public interface IOrderFactory
    {
        Task<OrderListModel> PrepareOrderListModelAsync(OrderListRequestModel searchModel);
        Task<OrderListModel> PrepareOrderListModelAsync(UserOrderHistoryRequestModel searchModel);
        Task<OrderDetailModel> GetOrderDetailModelAsync(Order order
                                                        , bool isUserDetail = false
                                                        , bool isCouponDetail = false
                                                        , bool isOrderProductItem = false
                                                        , bool isOrderProductItemDetail = false
                                                        , bool isProductTicketCategoryMapDetail = false);
        Task<OrderProductItemModel>  GetOrderProductItemModelAsync(OrderProductItem orderProductItem
                                                                  , bool isProductDetail = false
                                                                  , bool isOrderProductItemDetail=false);
        OrderModel MapOrderModelFromShoppingModel(ShoppingCartModel model);
        List<OrderProductItemModel> MapOrderProductItemModelFromShoppingItemModel(List<ShoppingCartItemModel> shoppingCartItems);
        Task<OrderCouponInfoModel> GetAndValidateCouponInfoByCode(OrderModel orderModel);
        Task<IList<Product>> GetAndValidateProductDetails(List<OrderProductItemModel> items);
        Task<OrderProductItemDetailModel> GetOrderProductItemDetailModelAsync(OrderProductItemDetail orderProductItemDetail
            , bool isQRCodeFile = false);

        Task<bool> CheckProductTicketAvaibility(IList<Product> productDetails, List<OrderProductItemModel> orderProductRequestItems);
        Task<UserProductItemDetail> GetOrderProductItemDetailModelAsync(OrderProductItemDetail orderProductItemDetail);
    }
}
