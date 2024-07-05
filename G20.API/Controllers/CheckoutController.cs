using G20.API.Factories.Media;
using G20.API.Factories.Orders;
using G20.API.Factories.ShoppingCarts;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.BoardingDetails;
using G20.API.Models.Checkout;
using G20.API.Models.Media;
using G20.API.Models.Orders;
using G20.API.Models.ShoppingCarts;
using G20.Core;
using G20.Core.Domain;
using G20.Core.Enums;
using G20.Service.Orders;
using G20.Service.Payments;
using G20.Service.ProductCombos;
using G20.Service.Products;
using G20.Service.ProductTicketCategoriesMap;
using G20.Service.QRCodes;
using G20.Service.ShoppingCarts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Nop.Core;
using QRCoder;

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
        protected readonly IPaymentService _paymentService;
        protected readonly IOrderProcessingService _orderProcessingService;

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
            , IQRCodeService qrCodeService
            , IPaymentService paymentService
            , IOrderProcessingService orderProcessingService)
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
            _paymentService = paymentService;
            _orderProcessingService = orderProcessingService;
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

        private async Task<bool> ClearShoppingCart(int userId)
        {
            #region Clear Shopping Cart

            var shoppingCart = await _shoppingCartService.GetByUserIdAsync(userId);
            await _shoppingCartService.DeleteAsync(shoppingCart);

            #endregion

            return true;
        }

        private async Task<bool> UpdateProductTicketCategoryStock(int orderId)
        {
            #region Update product stock
            var orderProductItems = (await _orderProductItemService.GetOrderProductItemsAsync(orderId)).ToList();
            foreach (var orderProductItem in orderProductItems)
            {
                var productTicketCategoryMap = await _productTicketCategoryMapService.GetByIdAsync(orderProductItem.ProductTicketCategoryMapId);
                productTicketCategoryMap.Sold = productTicketCategoryMap.Sold + orderProductItem.Quantity;
                await _productTicketCategoryMapService.UpdateAsync(productTicketCategoryMap);
            }

            #endregion
            return true;
        }

        #endregion

        [HttpPost]
        public virtual async Task<IActionResult> Post(CheckoutRequestModel model)
        {
            var userId = _workContext.GetCurrentUserId();

            var shoppingCartModel = await _shoppingCartFactory.GetShoppingCartDetailByUserId(userId);
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
            order.OrderStatusId = (int)OrderStatusEnum.New;
            order.PaymentTypeId = (int)model.PaymentTypeId;
            order.PaymentStatusId = (int)PaymentStatus.Authorized;
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
                                    , productCombo.ProductMapId
                                    , productCombo.Id);
                            orderProductItemDetails.Add(orderProductComboItemDetail);
                        }
                        break;
                }

                foreach (var orderProductItemDetail in orderProductItemDetails)
                {

                    await _orderProductItemDetailService.InsertAsync(orderProductItemDetail);

                    string qrCode = string.Format("{0}-{1}-{2}", orderProductItemDetail.Id, item.ProductId, item.ProductTicketCategoryMapId); ;
                    //Create QR code
                    BoardingCheckDetailModel boardingCheckDetailModel = new BoardingCheckDetailModel();
                    boardingCheckDetailModel.OrderProductItemDetailId = orderProductItemDetail.Id;
                    boardingCheckDetailModel.ProductId = item.ProductId;
                    boardingCheckDetailModel.ProductTicketCategoryMapId = item.ProductTicketCategoryMapId;
                    boardingCheckDetailModel.Quantity = item.Quantity;

                    var qrFileContentArray = await _qrCodeService.GenerateQRCode(qrCode);
                    FileUploadRequestModel fileUploadRequestModel = new FileUploadRequestModel();
                    fileUploadRequestModel.FileAsBase64 = Convert.ToBase64String(qrFileContentArray);
                    fileUploadRequestModel.FileType = FileTypeEnum.OrderProductItemQRCode;
                    fileUploadRequestModel.FileName = string.Format("{0}_{1}.png", FileTypeEnum.OrderProductItemQRCode.ToString(), orderProductItemDetail.Id);
                    var file = await _mediaFactoryModel.UploadRequestModelAsync(fileUploadRequestModel);

                    //Update QR code file id
                    var orderProductItemDetailEntity = await _orderProductItemDetailService.GetByIdAsync(orderProductItemDetail.Id);
                    orderProductItemDetailEntity.QRCodeFileId = file.Id;
                    orderProductItemDetailEntity.QRCode = qrCode;
                    await _orderProductItemDetailService.UpdateAsync(orderProductItemDetailEntity);

                }

                #endregion
            }

            #endregion

            var paymentMethod = PaymentServiceManager.GetPaymentMethod((PaymentTypeEnum)order.PaymentTypeId);
            if (paymentMethod == null)
            {
                throw new NopException("in valid");
            }

            if (paymentMethod.SkipPaymentInfo)
            {
                ProcessPaymentRequest processPaymentRequest = new ProcessPaymentRequest();
                processPaymentRequest.UserId = userId;
                processPaymentRequest.OrderId = order.Id;
                processPaymentRequest.PaymentType = (PaymentTypeEnum)order.PaymentTypeId;
                return await PostProcessPaymentRequest(processPaymentRequest);
            }
            return Success(orderModel);
        }

        [HttpPost]
        public virtual async Task<IActionResult> PostProcessPaymentRequest(ProcessPaymentRequest model)
        {
            var order = await _orderService.GetByIdAsync(model.OrderId);
            if (order == null)
            {
                throw new NopException("Order not found");
            }
            var paymentMethod = PaymentServiceManager.GetPaymentMethod((PaymentTypeEnum)order.PaymentTypeId);
            if (paymentMethod == null)
            {
                throw new NopException("in valid");
            }
            if (model == null)
            {
                model = new ProcessPaymentRequest();
            }

            await _orderService.UpdateOrderStatus(order, OrderStatusEnum.PaymentInitiate);
            await _paymentService.GenerateOrderGuidAsync(model);

            var placeOrderResult = await _orderProcessingService.PlaceOrderAsync(model);
            if (placeOrderResult.Success)
            {
                await _orderService.UpdateOrderStatus(order, OrderStatusEnum.PaymentInitiate);
                await _orderService.UpdatePaymentStatus(order, PaymentStatus.Paid);

                #region Clear Shopping Cart

                await ClearShoppingCart(model.UserId);

                #endregion

                #region Updated Product Ticket Category Stock

                await UpdateProductTicketCategoryStock(order.Id);

                #endregion

                #region Update order status

                await _orderService.UpdateOrderStatus(order, OrderStatusEnum.Completed);

                #endregion

                #region Send Email Notifications

                var isSend = await _orderService.SendOrderNotifications(order.Id);

                #endregion

                placeOrderResult.OrderId = order.Id;

                return Success(placeOrderResult);
            }
            else
            {
                await _orderService.UpdateOrderStatus(order, OrderStatusEnum.PaymentFailed);
                await _orderService.UpdatePaymentStatus(order, PaymentStatus.Pending);
            }

            return Error(placeOrderResult.Errors);
        }
    }
}
