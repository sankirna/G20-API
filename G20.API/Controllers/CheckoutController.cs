using G20.API.Factories.Media;
using G20.API.Factories.Orders;
using G20.API.Factories.ShoppingCarts;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.BoardingDetails;
using G20.API.Models.Checkout;
using G20.API.Models.Media;
using G20.API.Models.ShoppingCarts;
using G20.Core;
using G20.Core.Domain;
using G20.Core.Enums;
using G20.Service.Orders;
using G20.Service.ProductCombos;
using G20.Service.Products;
using G20.Service.ProductTicketCategoriesMap;
using G20.Service.QRCodes;
using G20.Service.ShoppingCarts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Nop.Core;

namespace G20.API.Controllers
{
    public class CheckoutController : BaseController
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

        public CheckoutController(IWorkContext workContext
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

        #region Private Method(s)

        private OrderProductItemDetail PrepareOrderProductItemDetail(
            int userId,
            int orderProductItemId,
            int productId,
            int? productComboId)
        {
            OrderProductItemDetail orderProductItemDetail = new OrderProductItemDetail()
            {
                UserId = userId,
                OrderProductItemId = orderProductItemId,
                ProductId = productId,
                ProductComboId = productComboId,
            };

            return orderProductItemDetail;
        }

        #endregion

        [HttpPost]
        public virtual async Task<IActionResult> Post(CheckoutRequestModel model)
        {
            var userId = _workContext.GetCurrentUserId();

            var shoppingCartModel = await _shoppingCartFactory.GetShoppingCartDetailByUserId(_workContext.GetCurrentUserId());
            if (shoppingCartModel == null)
            {
                throw new NopException("Please add some item(s) in cart");
            }

            #region Check validation and map order model from shopping cart

            var orderModel = _orderFactory.MapOrderModelFromShoppingModel(shoppingCartModel);
            var couponCodeInfo = await _orderFactory.GetAndValidateCouponInfoByCode(orderModel);
            if (couponCodeInfo != null)
            {
                orderModel.CouponId = couponCodeInfo.CouponId;
            }
            else
            {
                orderModel.CouponId = null;
                orderModel.CouponCode = null;
            }
            var productDetails = await _orderFactory.GetAndValidateProductDetails(orderModel.Items);
            var checkAvaibility = await _orderFactory.CheckProductTicketAvaibility(productDetails, orderModel.Items);
            if (!checkAvaibility)
            {
                throw new NopException("Some of product(s) are out of stock. please remove from cart");
            }

            #endregion

            #region Insert Order

            var order = orderModel.ToEntity<Order>();
            order.UserId = userId;
            order.Email = model.Email;
            order.Name = model.Name;
            order.PhoneNumber = model.PhoneNumber;

            await _orderService.InsertAsync(order);

            #endregion

            #region Insert Order Item

            foreach (var item in orderModel.Items)
            {
                var orderProductItem = item.ToEntity<OrderProductItem>();
                orderProductItem.OrderId = order.Id;
                orderProductItem.UserId = userId;



                await _orderProductItemService.InsertAsync(orderProductItem);

                #region Insert Order Item details

                List<OrderProductItemDetail> orderProductItemDetails = new List<OrderProductItemDetail>();

                switch (item.ProductDetail.ProductTypeEnum)
                {
                    case ProductTypeEnum.Regular:
                        var orderRegularProductItemDetail = PrepareOrderProductItemDetail(userId
                                    , orderProductItem.Id
                                    , item.ProductId
                                    , null);
                        orderProductItemDetails.Add(orderRegularProductItemDetail);
                        break;
                    case ProductTypeEnum.Combo:
                        var productCombos = await _productComboService.GetProductCombosByProductIdAsync(item.ProductId);
                        foreach (var productCombo in productCombos)
                        {
                            var orderProductComboItemDetail = PrepareOrderProductItemDetail(userId
                                    , orderProductItem.Id
                                    , item.ProductId
                                    , productCombo.Id);
                            orderProductItemDetails.Add(orderProductComboItemDetail);
                        }
                        break;
                }

                foreach (var orderProductItemDetail in orderProductItemDetails)
                {

                    await _orderProductItemDetailService.InsertAsync(orderProductItemDetail);

                    //Create QR code
                    BoardingCheckDetailModel boardingCheckDetailModel = new BoardingCheckDetailModel();
                    boardingCheckDetailModel.OrderProductItemDetailId = orderProductItemDetail.Id;
                    boardingCheckDetailModel.ProductId = item.ProductId;
                    boardingCheckDetailModel.ProductTicketCategoryMapId = item.ProductTicketCategoryMapId;
                    boardingCheckDetailModel.Quantity = item.Quantity;

                    var qrFileContentArray = await _qrCodeService.GenerateQRCode(boardingCheckDetailModel);
                    FileUploadRequestModel fileUploadRequestModel = new FileUploadRequestModel();
                    fileUploadRequestModel.FileAsBase64 = Convert.ToBase64String(qrFileContentArray);
                    fileUploadRequestModel.FileType = FileTypeEnum.OrderProductItemQRCode;
                    fileUploadRequestModel.FileName = string.Format("{0}_{1}.png", FileTypeEnum.OrderProductItemQRCode.ToString(), orderProductItemDetail.Id);
                    var file = await _mediaFactoryModel.UploadRequestModelAsync(fileUploadRequestModel);

                    //Update QR code file id
                    var orderProductItemDetailEntity = await _orderProductItemDetailService.GetByIdAsync(orderProductItemDetail.Id);
                    orderProductItemDetailEntity.QRCodeFileId = file.Id;
                    await _orderProductItemDetailService.UpdateAsync(orderProductItemDetailEntity);

                }

                #endregion

                #region Update product stock

                var productTicketCategoryMap = await _productTicketCategoryMapService.GetByIdAsync(item.ProductTicketCategoryMapId);
                productTicketCategoryMap.Sold = productTicketCategoryMap.Sold + item.Quantity;
                await _productTicketCategoryMapService.UpdateAsync(productTicketCategoryMap);

                #endregion

                #region Clear Shopping Cart

                var shoppingCart = await _shoppingCartService.GetByUserIdAsync(userId);
                await _shoppingCartService.DeleteAsync(shoppingCart);

                #endregion
            }

            #endregion

            return Success(orderModel);
        }
    }
}
