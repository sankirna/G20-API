using G20.Core.Domain;
using Microsoft.AspNetCore.Http;
using Nop.Core;
using System.Xml.Serialization;
using System.Xml;
using G20.Core.Enums;
using Nop.Services.Payments;
using G20.Service.Orders;

namespace G20.Service.Payments
{
    public partial class PaymentService : IPaymentService
    {
        #region Fields

        protected readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor

        public PaymentService(
            IHttpContextAccessor httpContextAccessor
            )
        {
            _httpContextAccessor = httpContextAccessor;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Process a payment
        /// </summary>
        /// <param name="processPaymentRequest">Payment info required for an order processing</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the process payment result
        /// </returns>
        public virtual async Task<ProcessPaymentResult> ProcessPaymentAsync(ProcessPaymentRequest processPaymentRequest)
        {
            if (processPaymentRequest.OrderTotal == decimal.Zero)
            {
                var result = new ProcessPaymentResult
                {
                    NewPaymentStatus = PaymentStatus.Paid
                };
                return result;
            }

            //We should strip out any white space or dash in the CC number entered.
            if (!string.IsNullOrWhiteSpace(processPaymentRequest.CreditCardNumber))
            {
                processPaymentRequest.CreditCardNumber = processPaymentRequest.CreditCardNumber.Replace(" ", string.Empty);
                processPaymentRequest.CreditCardNumber = processPaymentRequest.CreditCardNumber.Replace("-", string.Empty);
            }

            //var customer = await _customerService.GetCustomerByIdAsync(processPaymentRequest.CustomerId);
            //var paymentMethod = await _paymentPluginManager
            //                        .LoadPluginBySystemNameAsync(processPaymentRequest.PaymentMethodSystemName, customer, processPaymentRequest.StoreId)
            //                    ?? throw new NopException("Payment method couldn't be loaded");
            var paymentMethod = PaymentServiceManager.GetPaymentMethod(processPaymentRequest.PaymentType);

            return await paymentMethod.ProcessPaymentAsync(processPaymentRequest);
        }

        /// <summary>
        /// Post process payment (used by payment gateways that require redirecting to a third-party URL)
        /// </summary>
        /// <param name="postProcessPaymentRequest">Payment info required for an order processing</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task PostProcessPaymentAsync(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            //already paid or order.OrderTotal == decimal.Zero
            if ((PaymentStatus)postProcessPaymentRequest.Order.PaymentStatusId == PaymentStatus.Paid)
                return;

            //var customer = await _customerService.GetCustomerByIdAsync(postProcessPaymentRequest.Order.CustomerId);
            //var paymentMethod = await _paymentPluginManager
            //                        .LoadPluginBySystemNameAsync(postProcessPaymentRequest.Order.PaymentMethodSystemName, customer, postProcessPaymentRequest.Order.StoreId)
            //                    ?? throw new NopException("Payment method couldn't be loaded");
            var paymentMethod = PaymentServiceManager.GetPaymentMethod(postProcessPaymentRequest.paymentType);//TODO
            await paymentMethod.PostProcessPaymentAsync(postProcessPaymentRequest);
        }

        /// <summary>
        /// Gets a value indicating whether customers can complete a payment after order is placed but not completed (for redirection payment methods)
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the result
        /// </returns>
        public virtual async Task<bool> CanRePostProcessPaymentAsync(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            //if (!_paymentSettings.AllowRePostingPayments)
            //    return false;

            var paymentMethod = PaymentServiceManager.GetPaymentMethod(order.ToPaymentType());//TODO
            if (paymentMethod == null)
                return false; //Payment method couldn't be loaded (for example, was uninstalled)

            if (paymentMethod.PaymentMethodType != PaymentMethodType.Redirection)
                return false;   //this option is available only for redirection payment methods

            //if (order.Deleted)
            //    return false;  //do not allow for deleted orders

            if (order.OrderStatusId == (int)OrderStatusEnum.Cancelled)
                return false;  //do not allow for cancelled orders

            if (order.PaymentStatusId != (int)PaymentStatus.Pending)
                return false;  //payment status should be Pending

            return await paymentMethod.CanRePostProcessPaymentAsync(order);
        }

        /// <summary>
        /// Gets an additional handling fee of a payment method
        /// </summary>
        /// <param name="cart">Shopping cart</param>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the additional handling fee
        /// </returns>
        public virtual async Task<decimal> GetAdditionalHandlingFeeAsync(IList<ShoppingCartItem> cart, string paymentMethodSystemName)
        {
            if (string.IsNullOrEmpty(paymentMethodSystemName))
                return decimal.Zero;

            var paymentMethod = PaymentServiceManager.GetPaymentMethod(paymentMethodSystemName.ToPaymentType());//TODO
            if (paymentMethod == null)
                return decimal.Zero;

            var result = await paymentMethod.GetAdditionalHandlingFeeAsync(cart);
            if (result < decimal.Zero)
                result = decimal.Zero;


            //result = await _priceCalculationService.RoundPriceAsync(result);

            return result;
        }

        /// <summary>
        /// Gets a value indicating whether capture is supported by payment method
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains a value indicating whether capture is supported
        /// </returns>
        public virtual async Task<bool> SupportCaptureAsync(string paymentMethodSystemName)
        {
            var paymentMethod = PaymentServiceManager.GetPaymentMethod(paymentMethodSystemName.ToPaymentType());//TODO
            if (paymentMethod == null)
                return false;
            return paymentMethod.SupportCapture;
        }

        /// <summary>
        /// Captures payment
        /// </summary>
        /// <param name="capturePaymentRequest">Capture payment request</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the capture payment result
        /// </returns>
        public virtual async Task<CapturePaymentResult> CaptureAsync(CapturePaymentRequest capturePaymentRequest)
        {
            var paymentMethod = PaymentServiceManager.GetPaymentMethod(capturePaymentRequest.Order.ToPaymentType());//TODO
            return await paymentMethod.CaptureAsync(capturePaymentRequest);
        }

        /// <summary>
        /// Gets a value indicating whether partial refund is supported by payment method
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains a value indicating whether partial refund is supported
        /// </returns>
        public virtual async Task<bool> SupportPartiallyRefundAsync(string paymentMethodSystemName)
        {
            var paymentMethod = PaymentServiceManager.GetPaymentMethod(paymentMethodSystemName.ToPaymentType());//TODO
            if (paymentMethod == null)
                return false;
            return paymentMethod.SupportPartiallyRefund;
        }

        /// <summary>
        /// Gets a value indicating whether refund is supported by payment method
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains a value indicating whether refund is supported
        /// </returns>
        public virtual async Task<bool> SupportRefundAsync(string paymentMethodSystemName)
        {
            var paymentMethod = PaymentServiceManager.GetPaymentMethod(paymentMethodSystemName.ToPaymentType());//TODO
            if (paymentMethod == null)
                return false;
            return paymentMethod.SupportRefund;
        }

        /// <summary>
        /// Refunds a payment
        /// </summary>
        /// <param name="refundPaymentRequest">Request</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the result
        /// </returns>
        public virtual async Task<RefundPaymentResult> RefundAsync(RefundPaymentRequest refundPaymentRequest)
        {
            var paymentMethod = PaymentServiceManager.GetPaymentMethod(refundPaymentRequest.Order.ToPaymentType());//TODO
            return await paymentMethod.RefundAsync(refundPaymentRequest);
        }

        /// <summary>
        /// Gets a value indicating whether void is supported by payment method
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains a value indicating whether void is supported
        /// </returns>
        public virtual async Task<bool> SupportVoidAsync(string paymentMethodSystemName)
        {
            var paymentMethod = PaymentServiceManager.GetPaymentMethod(paymentMethodSystemName.ToPaymentType());//TODO
            if (paymentMethod == null)
                return false;
            return paymentMethod.SupportVoid;
        }

        /// <summary>
        /// Voids a payment
        /// </summary>
        /// <param name="voidPaymentRequest">Request</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the result
        /// </returns>
        public virtual async Task<VoidPaymentResult> VoidAsync(VoidPaymentRequest voidPaymentRequest)
        {
            var paymentMethod = PaymentServiceManager.GetPaymentMethod(voidPaymentRequest.Order.ToPaymentType());//TODO

            return await paymentMethod.VoidAsync(voidPaymentRequest);
        }

        /// <summary>
        /// Gets a recurring payment type of payment method
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains a recurring payment type of payment method
        /// </returns>
        public virtual async Task<RecurringPaymentType> GetRecurringPaymentTypeAsync(string paymentMethodSystemName)
        {
            var paymentMethod = PaymentServiceManager.GetPaymentMethod(paymentMethodSystemName.ToPaymentType());//TODO
            if (paymentMethod == null)
                return RecurringPaymentType.NotSupported;

            return paymentMethod.RecurringPaymentType;
        }

        /// <summary>
        /// Process recurring payment
        /// </summary>
        /// <param name="processPaymentRequest">Payment info required for an order processing</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the process payment result
        /// </returns>
        public virtual async Task<ProcessPaymentResult> ProcessRecurringPaymentAsync(ProcessPaymentRequest processPaymentRequest)
        {
            if (processPaymentRequest.OrderTotal == decimal.Zero)
            {
                var result = new ProcessPaymentResult
                {
                    NewPaymentStatus = PaymentStatus.Paid
                };
                return result;
            }

            var paymentMethod = PaymentServiceManager.GetPaymentMethod(processPaymentRequest.PaymentType);//TODO

            return await paymentMethod.ProcessRecurringPaymentAsync(processPaymentRequest);
        }

        /// <summary>
        /// Cancels a recurring payment
        /// </summary>
        /// <param name="cancelPaymentRequest">Request</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the result
        /// </returns>
        public virtual async Task<CancelRecurringPaymentResult> CancelRecurringPaymentAsync(CancelRecurringPaymentRequest cancelPaymentRequest)
        {
            if (cancelPaymentRequest.Order.GrandTotal == decimal.Zero)
                return new CancelRecurringPaymentResult();

            var paymentMethod = PaymentServiceManager.GetPaymentMethod(cancelPaymentRequest.Order.ToPaymentType());//TODO

            return await paymentMethod.CancelRecurringPaymentAsync(cancelPaymentRequest);
        }

        /// <summary>
        /// Gets masked credit card number
        /// </summary>
        /// <param name="creditCardNumber">Credit card number</param>
        /// <returns>Masked credit card number</returns>
        public virtual string GetMaskedCreditCardNumber(string creditCardNumber)
        {
            if (string.IsNullOrEmpty(creditCardNumber))
                return string.Empty;

            if (creditCardNumber.Length <= 4)
                return creditCardNumber;

            var last4 = creditCardNumber[(creditCardNumber.Length - 4)..creditCardNumber.Length];
            var maskedChars = string.Empty;
            for (var i = 0; i < creditCardNumber.Length - 4; i++)
            {
                maskedChars += "*";
            }

            return maskedChars + last4;
        }

        /// <summary>
        /// Serialize CustomValues of ProcessPaymentRequest
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Serialized CustomValues</returns>
        public virtual string SerializeCustomValues(ProcessPaymentRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (!request.CustomValues.Any())
                return null;

            //XmlSerializer won't serialize objects that implement IDictionary by default.
            //http://msdn.microsoft.com/en-us/magazine/cc164135.aspx 

            //also see http://ropox.ru/tag/ixmlserializable/ (Russian language)

            var ds = new DictionarySerializer(request.CustomValues);
            var xs = new XmlSerializer(typeof(DictionarySerializer));

            using var textWriter = new StringWriter();
            using (var xmlWriter = XmlWriter.Create(textWriter))
            {
                xs.Serialize(xmlWriter, ds);
            }

            var result = textWriter.ToString();
            return result;
        }

        /// <summary>
        /// Deserialize CustomValues of Order
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>Serialized CustomValues CustomValues</returns>
        public virtual Dictionary<string, object> DeserializeCustomValues(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            //if (string.IsNullOrWhiteSpace(order.CustomValuesXml))
            //    return new Dictionary<string, object>();

            //var serializer = new XmlSerializer(typeof(DictionarySerializer));

            //using var textReader = new StringReader(order.CustomValuesXml);
            //using var xmlReader = XmlReader.Create(textReader);
            //if (serializer.Deserialize(xmlReader) is DictionarySerializer ds)
            //    return ds.Dictionary;
            return [];
        }

        /// <summary>
        /// Generate an order GUID
        /// </summary>
        /// <param name="processPaymentRequest">Process payment request</param>
        public virtual async Task GenerateOrderGuidAsync(ProcessPaymentRequest processPaymentRequest)
        {
            if (processPaymentRequest == null)
                return;

            //we should use the same GUID for multiple payment attempts
            //this way a payment gateway can prevent security issues such as credit card brute-force attacks
            //in order to avoid any possible limitations by payment gateway we reset GUID periodically
            var previousPaymentRequest = new ProcessPaymentRequest();// await _httpContextAccessor.HttpContext.Session.GetAsync<ProcessPaymentRequest>("OrderPaymentInfo");
            //if (_paymentSettings.RegenerateOrderGuidInterval > 0 &&
            //    previousPaymentRequest != null &&
            //    previousPaymentRequest.OrderGuidGeneratedOnUtc.HasValue)
            //{
            //    var interval = DateTime.UtcNow - previousPaymentRequest.OrderGuidGeneratedOnUtc.Value;
            //    if (interval.TotalSeconds < _paymentSettings.RegenerateOrderGuidInterval)
            //    {
            //        processPaymentRequest.OrderGuid = previousPaymentRequest.OrderGuid;
            //        processPaymentRequest.OrderGuidGeneratedOnUtc = previousPaymentRequest.OrderGuidGeneratedOnUtc;
            //    }
            //}

            if (processPaymentRequest.OrderGuid == Guid.Empty)
            {
                processPaymentRequest.OrderGuid = Guid.NewGuid();
                processPaymentRequest.OrderGuidGeneratedOnUtc = DateTime.UtcNow;
            }
        }

        #endregion
    }
}
