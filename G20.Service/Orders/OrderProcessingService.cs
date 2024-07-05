using G20.Core.Domain;
using G20.Service.Payments;
using Nop.Core;

namespace G20.Service.Orders
{
    public class OrderProcessingService : IOrderProcessingService
    {

        #region Utilities

        /// <summary>
        /// Prepare details to place an order. It also sets some properties to "processPaymentRequest"
        /// </summary>
        /// <param name="processPaymentRequest">Process payment request</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the details
        /// </returns>
        protected virtual async Task<PlaceOrderContainer> PreparePlaceOrderDetailsAsync(ProcessPaymentRequest processPaymentRequest)
        {
            var details = new PlaceOrderContainer();

            //var affiliate = await _affiliateService.GetAffiliateByIdAsync(details.Customer.AffiliateId);
            //if (affiliate != null && affiliate.Active && !affiliate.Deleted)
            //    details.AffiliateId = affiliate.Id;

            return details;
        }

        /// <summary>
        /// Get process payment result
        /// </summary>
        /// <param name="processPaymentRequest">Process payment request</param>
        /// <param name="details">Place order container</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the 
        /// </returns>
        protected virtual async Task<ProcessPaymentResult> GetProcessPaymentResultAsync(ProcessPaymentRequest processPaymentRequest, PlaceOrderContainer details)
        {
            //process payment
            ProcessPaymentResult processPaymentResult;
            var _paymentService = PaymentServiceManager.GetPaymentMethod(processPaymentRequest.PaymentType);
            processPaymentResult = await _paymentService.ProcessPaymentAsync(processPaymentRequest);
            return processPaymentResult;
        }



        #endregion
        public virtual async Task<PlaceOrderResult> PlaceOrderAsync(ProcessPaymentRequest processPaymentRequest)
        {
            ArgumentNullException.ThrowIfNull(processPaymentRequest);

            var result = new PlaceOrderResult();
            try
            {
                if (processPaymentRequest.OrderGuid == Guid.Empty)
                    throw new Exception("Order GUID is not generated");

                //prepare order details
                var details = await PreparePlaceOrderDetailsAsync(processPaymentRequest);

                var processPaymentResult = await GetProcessPaymentResultAsync(processPaymentRequest, details)
                                           ?? throw new NopException("processPaymentResult is not available");

                if (processPaymentResult.Success)
                {
                    //var order = await SaveOrderDetailsAsync(processPaymentRequest, processPaymentResult, details);
                    //result.PlacedOrder = order;

                    //move shopping cart items to order items
                    //await MoveShoppingCartItemsToOrderItemsAsync(details, order);

                    //discount usage history
                    //await SaveDiscountUsageHistoryAsync(details, order);

                    //gift card usage history
                    //await SaveGiftCardUsageHistoryAsync(details, order);

                    //recurring orders
                    //if (details.IsRecurringShoppingCart)
                    //    await CreateFirstRecurringPaymentAsync(processPaymentRequest, order);

                    //notifications
                    //await SendNotificationsAndSaveNotesAsync(order);

                    //reset checkout data
                    //await _customerService.ResetCheckoutDataAsync(details.Customer, processPaymentRequest.StoreId, clearCouponCodes: true, clearCheckoutAttributes: true);
                    //await _customerActivityService.InsertActivityAsync("PublicStore.PlaceOrder",
                    //    string.Format(await _localizationService.GetResourceAsync("ActivityLog.PublicStore.PlaceOrder"), order.Id), order);

                    //raise event       
                    //await _eventPublisher.PublishAsync(new OrderPlacedEvent(order));

                    //check order status
                    //await CheckOrderStatusAsync(order);

                    //if (order.PaymentStatus == PaymentStatus.Paid)
                    //    await ProcessOrderPaidAsync(order);
                }
                else
                    foreach (var paymentError in processPaymentResult.Errors)
                        result.AddError(string.Format("Checkout.PaymentErro: {0}", string.Join(paymentError, ",")));
            }
            catch (Exception exc)
            {
                //await _logger.ErrorAsync(exc.Message, exc);
                result.AddError(exc.Message);
            }

            if (result.Success)
                return result;

            //log errors
            var logError = result.Errors.Aggregate("Error while placing order. ",
                (current, next) => $"{current}Error {result.Errors.IndexOf(next) + 1}: {next}. ");

            return result;
        }


        #region Nested class

        /// <summary>
        /// PlaceOrder container
        /// </summary>
        protected partial class PlaceOrderContainer
        {
            public PlaceOrderContainer()
            {
                Cart = new List<ShoppingCartItem>();
            }

            /// <summary>
            /// Customer
            /// </summary>
            public User User { get; set; }


            /// <summary>
            /// Affiliate identifier
            /// </summary>
            public int AffiliateId { get; set; }

            /// <summary>
            /// Initial order (used with recurring payments)
            /// </summary>
            public Order InitialOrder { get; set; }

            public IList<ShoppingCartItem> Cart { get; set; }

            /// <summary>
            /// Order subtotal (incl tax)
            /// </summary>
            public decimal OrderSubTotalInclTax { get; set; }

            /// <summary>
            /// Order subtotal (excl tax)
            /// </summary>
            public decimal OrderSubTotalExclTax { get; set; }

            /// <summary>
            /// Subtotal discount (incl tax)
            /// </summary>
            public decimal OrderSubTotalDiscountInclTax { get; set; }

            /// <summary>
            /// Subtotal discount (excl tax)
            /// </summary>
            public decimal OrderSubTotalDiscountExclTax { get; set; }

            /// <summary>
            /// Shipping (incl tax)
            /// </summary>
            public decimal OrderShippingTotalInclTax { get; set; }

            /// <summary>
            /// Shipping (excl tax)
            /// </summary>
            public decimal OrderShippingTotalExclTax { get; set; }

            /// <summary>
            /// Payment additional fee (incl tax)
            /// </summary>
            public decimal PaymentAdditionalFeeInclTax { get; set; }

            /// <summary>
            /// Payment additional fee (excl tax)
            /// </summary>
            public decimal PaymentAdditionalFeeExclTax { get; set; }

            /// <summary>
            /// Tax
            /// </summary>
            public decimal OrderTaxTotal { get; set; }

            /// <summary>
            /// VAT number
            /// </summary>
            public string VatNumber { get; set; }

            /// <summary>
            /// Tax rates
            /// </summary>
            public string TaxRates { get; set; }

            /// <summary>
            /// Order total discount amount
            /// </summary>
            public decimal OrderDiscountAmount { get; set; }

            /// <summary>
            /// Redeemed reward points
            /// </summary>
            public int RedeemedRewardPoints { get; set; }

            /// <summary>
            /// Redeemed reward points amount
            /// </summary>
            public decimal RedeemedRewardPointsAmount { get; set; }

            /// <summary>
            /// Order total
            /// </summary>
            public decimal OrderTotal { get; set; }
        }

        #endregion
    }
}
