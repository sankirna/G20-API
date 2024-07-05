using G20.Core.Domain;
using G20.Core.Enums;
using G20.Service.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Payments.ManualPayment
{
    public class CashPaymentProcessor : IPaymentMethod
    {
        #region Fields


        #endregion

        #region Ctor

        public CashPaymentProcessor()
        {

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
        public Task<ProcessPaymentResult> ProcessPaymentAsync(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new ProcessPaymentResult();
            switch (TransactMode.Authorize)//TODO Check
            {
                case TransactMode.Pending:
                    result.NewPaymentStatus = PaymentStatus.Pending;
                    break;
                case TransactMode.Authorize:
                    result.NewPaymentStatus = PaymentStatus.Authorized;
                    break;
                case TransactMode.AuthorizeAndCapture:
                    result.NewPaymentStatus = PaymentStatus.Paid;
                    break;
                default:
                    result.AddError("Not supported transaction type");
                    break;
            }

            return Task.FromResult(result);
        }

        /// <summary>
        /// Post process payment (used by payment gateways that require redirecting to a third-party URL)
        /// </summary>
        /// <param name="postProcessPaymentRequest">Payment info required for an order processing</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public Task PostProcessPaymentAsync(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            //nothing
            return Task.CompletedTask;
        }

        /// <summary>
        /// Returns a value indicating whether payment method should be hidden during checkout
        /// </summary>
        /// <param name="cart">Shopping cart</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains true - hide; false - display.
        /// </returns>
        public Task<bool> HidePaymentMethodAsync(IList<ShoppingCartItem> cart)
        {
            //you can put any logic here
            //for example, hide this payment method if all products in the cart are downloadable
            //or hide this payment method if current customer is from certain country
            return Task.FromResult(false);
        }

        /// <summary>
        /// Gets additional handling fee
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the additional handling fee
        /// </returns>
        public async Task<decimal> GetAdditionalHandlingFeeAsync(IList<ShoppingCartItem> cart)
        {
            //return await _orderTotalCalculationService.CalculatePaymentAdditionalFeeAsync(cart,
            //    _manualPaymentSettings.AdditionalFee, _manualPaymentSettings.AdditionalFeePercentage);

            return 0;
        }

        /// <summary>
        /// Captures payment
        /// </summary>
        /// <param name="capturePaymentRequest">Capture payment request</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the capture payment result
        /// </returns>
        public Task<CapturePaymentResult> CaptureAsync(CapturePaymentRequest capturePaymentRequest)
        {
            return Task.FromResult(new CapturePaymentResult { Errors = new[] { "Capture method not supported" } });
        }

        /// <summary>
        /// Refunds a payment
        /// </summary>
        /// <param name="refundPaymentRequest">Request</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the result
        /// </returns>
        public Task<RefundPaymentResult> RefundAsync(RefundPaymentRequest refundPaymentRequest)
        {
            return Task.FromResult(new RefundPaymentResult { Errors = new[] { "Refund method not supported" } });
        }

        /// <summary>
        /// Voids a payment
        /// </summary>
        /// <param name="voidPaymentRequest">Request</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the result
        /// </returns>
        public Task<VoidPaymentResult> VoidAsync(VoidPaymentRequest voidPaymentRequest)
        {
            return Task.FromResult(new VoidPaymentResult { Errors = new[] { "Void method not supported" } });
        }

        /// <summary>
        /// Process recurring payment
        /// </summary>
        /// <param name="processPaymentRequest">Payment info required for an order processing</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the process payment result
        /// </returns>
        public Task<ProcessPaymentResult> ProcessRecurringPaymentAsync(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new ProcessPaymentResult
            {
                AllowStoringCreditCardNumber = true
            };
            switch (TransactMode.Authorize)//TODO Check
            {
                case TransactMode.Pending:
                    result.NewPaymentStatus = PaymentStatus.Pending;
                    break;
                case TransactMode.Authorize:
                    result.NewPaymentStatus = PaymentStatus.Authorized;
                    break;
                case TransactMode.AuthorizeAndCapture:
                    result.NewPaymentStatus = PaymentStatus.Paid;
                    break;
                default:
                    result.AddError("Not supported transaction type");
                    break;
            }

            return Task.FromResult(result);
        }

        /// <summary>
        /// Cancels a recurring payment
        /// </summary>
        /// <param name="cancelPaymentRequest">Request</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the result
        /// </returns>
        public Task<CancelRecurringPaymentResult> CancelRecurringPaymentAsync(CancelRecurringPaymentRequest cancelPaymentRequest)
        {
            //always success
            return Task.FromResult(new CancelRecurringPaymentResult());
        }

        /// <summary>
        /// Gets a value indicating whether customers can complete a payment after order is placed but not completed (for redirection payment methods)
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the result
        /// </returns>
        public Task<bool> CanRePostProcessPaymentAsync(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            //it's not a redirection payment method. So we always return false
            return Task.FromResult(false);
        }

        /// <summary>
        /// Validate payment form
        /// </summary>
        /// <param name="form">The parsed form values</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of validating errors
        /// </returns>
        public Task<IList<string>> ValidatePaymentFormAsync(IFormCollection form)
        {
            var warnings = new List<string>();

            //validate
            var validator = new PaymentInfoValidator();
            var model = new PaymentInfoModel();
            var validationResult = validator.Validate(model);
            if (!validationResult)
                //warnings.AddRange(validationResult.Errors.Select(error => error.ErrorMessage));
                warnings.AddRange(new List<string>() { "XXXXX"});

            return Task.FromResult<IList<string>>(warnings);
        }

        /// <summary>
        /// Get payment information
        /// </summary>
        /// <param name="form">The parsed form values</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the payment info holder
        /// </returns>
        public Task<ProcessPaymentRequest> GetPaymentInfoAsync(IFormCollection form)
        {
            return Task.FromResult(new ProcessPaymentRequest());
        }

        /// <summary>
        /// Gets a type of a view component for displaying plugin in public store ("payment info" checkout step)
        /// </summary>
        /// <returns>View component type</returns>
        public Type GetPublicViewComponent()
        {
            //return typeof(PaymentManualViewComponent);
            return null;
        }

        public Task<string> GetPaymentMethodDescriptionAsync()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether capture is supported
        /// </summary>
        public bool SupportCapture => false;

        /// <summary>
        /// Gets a value indicating whether partial refund is supported
        /// </summary>
        public bool SupportPartiallyRefund => false;

        /// <summary>
        /// Gets a value indicating whether refund is supported
        /// </summary>
        public bool SupportRefund => false;

        /// <summary>
        /// Gets a value indicating whether void is supported
        /// </summary>
        public bool SupportVoid => false;

        /// <summary>
        /// Gets a recurring payment type of payment method
        /// </summary>
        public RecurringPaymentType RecurringPaymentType => RecurringPaymentType.Manual;

        /// <summary>
        /// Gets a payment method type
        /// </summary>
        public PaymentMethodType PaymentMethodType => PaymentMethodType.Standard;

        /// <summary>
        /// Gets a value indicating whether we should display a payment information page for this plugin
        /// </summary>
        public bool SkipPaymentInfo => true;

        #endregion
    }
}
