using G20.Core;
using G20.Core.Domain;
using G20.Core.Enums;
using G20.Service.Files;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;


namespace G20.Service.Messages;

/// <summary>
/// Message token provider
/// </summary>
public partial class MessageTokenProvider : IMessageTokenProvider
{
    #region Fields

    protected readonly IActionContextAccessor _actionContextAccessor;
    protected readonly IUrlHelperFactory _urlHelperFactory;

    protected Dictionary<string, IEnumerable<string>> _allowedTokens;

    #endregion

    #region Ctor

    public MessageTokenProvider(
        IActionContextAccessor actionContextAccessor,
        IUrlHelperFactory urlHelperFactory
        )
    {
        _urlHelperFactory = urlHelperFactory;
    }

    #endregion

    #region Allowed tokens

    /// <summary>
    /// Get all available tokens by token groups
    /// </summary>
    protected Dictionary<string, IEnumerable<string>> AllowedTokens
    {
        get
        {
            if (_allowedTokens != null)
                return _allowedTokens;

            _allowedTokens = new Dictionary<string, IEnumerable<string>>
            {
                //store tokens
                {
                    TokenGroupNames.StoreTokens,
                    new[]
                    {
                        "%Store.Name%",
                        "%Store.URL%",
                        "%Store.Email%",
                        "%Store.CompanyName%",
                        "%Store.CompanyAddress%",
                        "%Store.CompanyPhoneNumber%",
                        "%Store.CompanyVat%",
                        "%Facebook.URL%",
                        "%Twitter.URL%",
                        "%YouTube.URL%",
                        "%Instagram.URL%"
                    }
                },

                //customer tokens
                {
                    TokenGroupNames.CustomerTokens,
                    new[]
                    {
                        "%Customer.Email%",
                        "%Customer.Username%",
                        "%Customer.FullName%",
                        "%Customer.FirstName%",
                        "%Customer.LastName%",
                        "%Customer.VatNumber%",
                        "%Customer.VatNumberStatus%",
                        "%Customer.CustomAttributes%",
                        "%Customer.PasswordRecoveryURL%",
                        "%Customer.AccountActivationURL%",
                        "%Customer.EmailRevalidationURL%",
                        "%Wishlist.URLForCustomer%"
                    }
                },

                //order tokens
                {
                    TokenGroupNames.OrderTokens,
                    new[]
                    {
                        "%Order.OrderNumber%",
                        "%Order.CustomerFullName%",
                        "%Order.CustomerEmail%",
                        "%Order.BillingFirstName%",
                        "%Order.BillingLastName%",
                        "%Order.BillingPhoneNumber%",
                        "%Order.BillingEmail%",
                        "%Order.BillingFaxNumber%",
                        "%Order.BillingCompany%",
                        "%Order.BillingAddress1%",
                        "%Order.BillingAddress2%",
                        "%Order.BillingCity%",
                        "%Order.BillingCounty%",
                        "%Order.BillingStateProvince%",
                        "%Order.BillingZipPostalCode%",
                        "%Order.BillingCountry%",
                        "%Order.BillingCustomAttributes%",
                        "%Order.BillingAddressLine%",
                        "%Order.Shippable%",
                        "%Order.ShippingMethod%",
                        "%Order.ShippingFirstName%",
                        "%Order.ShippingLastName%",
                        "%Order.ShippingPhoneNumber%",
                        "%Order.ShippingEmail%",
                        "%Order.ShippingFaxNumber%",
                        "%Order.ShippingCompany%",
                        "%Order.ShippingAddress1%",
                        "%Order.ShippingAddress2%",
                        "%Order.ShippingCity%",
                        "%Order.ShippingCounty%",
                        "%Order.ShippingStateProvince%",
                        "%Order.ShippingZipPostalCode%",
                        "%Order.ShippingCountry%",
                        "%Order.ShippingCustomAttributes%",
                        "%Order.ShippingAddressLine%",
                        "%Order.PaymentMethod%",
                        "%Order.VatNumber%",
                        "%Order.CustomValues%",
                        "%Order.Product(s)%",
                        "%Order.CreatedOn%",
                        "%Order.OrderURLForCustomer%",
                        "%Order.PickupInStore%",
                        "%Order.OrderId%",
                        "%Order.IsCompletelyShipped%",
                        "%Order.IsCompletelyReadyForPickup%",
                        "%Order.IsCompletelyDelivered%"
                    }
                },

                //shipment tokens
                {
                    TokenGroupNames.ShipmentTokens,
                    new[]
                    {
                        "%Shipment.ShipmentNumber%",
                        "%Shipment.TrackingNumber%",
                        "%Shipment.TrackingNumberURL%",
                        "%Shipment.Product(s)%",
                        "%Shipment.URLForCustomer%"
                    }
                },

                //refunded order tokens
                {
                    TokenGroupNames.RefundedOrderTokens,
                    new[]
                    {
                        "%Order.AmountRefunded%"
                    }
                },

                //order note tokens
                {
                    TokenGroupNames.OrderNoteTokens,
                    new[]
                    {
                        "%Order.NewNoteText%",
                        "%Order.OrderNoteAttachmentUrl%"
                    }
                },

                //recurring payment tokens
                {
                    TokenGroupNames.RecurringPaymentTokens,
                    new[]
                    {
                        "%RecurringPayment.ID%",
                        "%RecurringPayment.CancelAfterFailedPayment%",
                        "%RecurringPayment.RecurringPaymentType%"
                    }
                },

                //newsletter subscription tokens
                {
                    TokenGroupNames.SubscriptionTokens,
                    new[]
                    {
                        "%NewsLetterSubscription.Email%",
                        "%NewsLetterSubscription.ActivationUrl%",
                        "%NewsLetterSubscription.DeactivationUrl%"
                    }
                },

                //product tokens
                {
                    TokenGroupNames.ProductTokens,
                    new[]
                    {
                        "%Product.ID%",
                        "%Product.Name%",
                        "%Product.ShortDescription%",
                        "%Product.ProductURLForCustomer%",
                        "%Product.SKU%",
                        "%Product.StockQuantity%"
                    }
                },

                //return request tokens
                {
                    TokenGroupNames.ReturnRequestTokens,
                    new[]
                    {
                        "%ReturnRequest.CustomNumber%",
                        "%ReturnRequest.OrderId%",
                        "%ReturnRequest.Product.Quantity%",
                        "%ReturnRequest.Product.Name%",
                        "%ReturnRequest.Reason%",
                        "%ReturnRequest.RequestedAction%",
                        "%ReturnRequest.CustomerComment%",
                        "%ReturnRequest.StaffNotes%",
                        "%ReturnRequest.Status%"
                    }
                },

                //forum tokens
                {
                    TokenGroupNames.ForumTokens,
                    new[]
                    {
                        "%Forums.ForumURL%",
                        "%Forums.ForumName%"
                    }
                },

                //forum topic tokens
                {
                    TokenGroupNames.ForumTopicTokens,
                    new[]
                    {
                        "%Forums.TopicURL%",
                        "%Forums.TopicName%"
                    }
                },

                //forum post tokens
                {
                    TokenGroupNames.ForumPostTokens,
                    new[]
                    {
                        "%Forums.PostAuthor%",
                        "%Forums.PostBody%"
                    }
                },

                //private message tokens
                {
                    TokenGroupNames.PrivateMessageTokens,
                    new[]
                    {
                        "%PrivateMessage.Subject%",
                        "%PrivateMessage.Text%"
                    }
                },

                //vendor tokens
                {
                    TokenGroupNames.VendorTokens,
                    new[]
                    {
                        "%Vendor.Name%",
                        "%Vendor.Email%",
                        "%Vendor.VendorAttributes%"
                    }
                },

                //gift card tokens
                {
                    TokenGroupNames.GiftCardTokens,
                    new[]
                    {
                        "%GiftCard.SenderName%",
                        "%GiftCard.SenderEmail%",
                        "%GiftCard.RecipientName%",
                        "%GiftCard.RecipientEmail%",
                        "%GiftCard.Amount%",
                        "%GiftCard.CouponCode%",
                        "%GiftCard.Message%"
                    }
                },

                //product review tokens
                {
                    TokenGroupNames.ProductReviewTokens,
                    new[]
                    {
                        "%ProductReview.ProductName%",
                        "%ProductReview.Title%",
                        "%ProductReview.IsApproved%",
                        "%ProductReview.ReviewText%",
                        "%ProductReview.ReplyText%"
                    }
                },

                //attribute combination tokens
                {
                    TokenGroupNames.AttributeCombinationTokens,
                    new[]
                    {
                        "%AttributeCombination.Formatted%",
                        "%AttributeCombination.SKU%",
                        "%AttributeCombination.StockQuantity%"
                    }
                },

                //blog comment tokens
                {
                    TokenGroupNames.BlogCommentTokens,
                    new[]
                    {
                        "%BlogComment.BlogPostTitle%"
                    }
                },

                //news comment tokens
                {
                    TokenGroupNames.NewsCommentTokens,
                    new[]
                    {
                        "%NewsComment.NewsTitle%"
                    }
                },

                //product back in stock tokens
                {
                    TokenGroupNames.ProductBackInStockTokens,
                    new[]
                    {
                        "%BackInStockSubscription.ProductName%",
                        "%BackInStockSubscription.ProductUrl%"
                    }
                },

                //email a friend tokens
                {
                    TokenGroupNames.EmailAFriendTokens,
                    new[]
                    {
                        "%EmailAFriend.PersonalMessage%",
                        "%EmailAFriend.Email%"
                    }
                },

                //wishlist to friend tokens
                {
                    TokenGroupNames.WishlistToFriendTokens,
                    new[]
                    {
                        "%Wishlist.PersonalMessage%",
                        "%Wishlist.Email%"
                    }
                },

                //VAT validation tokens
                {
                    TokenGroupNames.VatValidation,
                    new[]
                    {
                        "%VatValidationResult.Name%",
                        "%VatValidationResult.Address%"
                    }
                },

                //contact us tokens
                {
                    TokenGroupNames.ContactUs,
                    new[]
                    {
                        "%ContactUs.SenderEmail%",
                        "%ContactUs.SenderName%",
                        "%ContactUs.Body%"
                    }
                },

                //contact vendor tokens
                {
                    TokenGroupNames.ContactVendor,
                    new[]
                    {
                        "%ContactUs.SenderEmail%",
                        "%ContactUs.SenderName%",
                        "%ContactUs.Body%"
                    }
                }
            };

            return _allowedTokens;
        }
    }

    #endregion

    #region Utilities

    ///// <summary>
    ///// Convert a collection to a HTML table
    ///// </summary>
    ///// <param name="order">Order</param>
    ///// <param name="languageId">Language identifier</param>
    ///// <param name="vendorId">Vendor identifier (used to limit products by vendor</param>
    ///// <returns>
    ///// A task that represents the asynchronous operation
    ///// The task result contains the hTML table of products
    ///// </returns>
    //protected virtual async Task<string> ProductListToHtmlTableAsync(Order order, int languageId, int vendorId)
    //{
    //    var language = await _languageService.GetLanguageByIdAsync(languageId);

    //    var sb = new StringBuilder();
    //    sb.AppendLine("<table border=\"0\" style=\"width:100%;\">");

    //    sb.AppendLine($"<tr style=\"background-color:{_templatesSettings.Color1};text-align:center;\">");
    //    sb.AppendLine($"<th>{await _localizationService.GetResourceAsync("Messages.Order.Product(s).Name", languageId)}</th>");
    //    sb.AppendLine($"<th>{await _localizationService.GetResourceAsync("Messages.Order.Product(s).Price", languageId)}</th>");
    //    sb.AppendLine($"<th>{await _localizationService.GetResourceAsync("Messages.Order.Product(s).Quantity", languageId)}</th>");
    //    sb.AppendLine($"<th>{await _localizationService.GetResourceAsync("Messages.Order.Product(s).Total", languageId)}</th>");
    //    sb.AppendLine("</tr>");

    //    var table = await _orderService.GetOrderItemsAsync(order.Id, vendorId: vendorId);
    //    for (var i = 0; i <= table.Count - 1; i++)
    //    {
    //        var orderItem = table[i];

    //        var product = await _productService.GetProductByIdAsync(orderItem.ProductId);

    //        if (product == null)
    //            continue;

    //        sb.AppendLine($"<tr style=\"background-color: {_templatesSettings.Color2};text-align: center;\">");
    //        //product name
    //        var productName = await _localizationService.GetLocalizedAsync(product, x => x.Name, languageId);

    //        sb.AppendLine("<td style=\"padding: 0.6em 0.4em;text-align: left;\">" + WebUtility.HtmlEncode(productName));

    //        //add download link
    //        if (await _orderService.IsDownloadAllowedAsync(orderItem))
    //        {
    //            var downloadUrl = await RouteUrlAsync(order.StoreId, "GetDownload", new { orderItemId = orderItem.OrderItemGuid });
    //            var downloadLink = $"<a class=\"link\" href=\"{downloadUrl}\">{await _localizationService.GetResourceAsync("Messages.Order.Product(s).Download", languageId)}</a>";
    //            sb.AppendLine("<br />");
    //            sb.AppendLine(downloadLink);
    //        }
    //        //add download link
    //        if (await _orderService.IsLicenseDownloadAllowedAsync(orderItem))
    //        {
    //            var licenseUrl = await RouteUrlAsync(order.StoreId, "GetLicense", new { orderItemId = orderItem.OrderItemGuid });
    //            var licenseLink = $"<a class=\"link\" href=\"{licenseUrl}\">{await _localizationService.GetResourceAsync("Messages.Order.Product(s).License", languageId)}</a>";
    //            sb.AppendLine("<br />");
    //            sb.AppendLine(licenseLink);
    //        }
    //        //attributes
    //        if (!string.IsNullOrEmpty(orderItem.AttributeDescription))
    //        {
    //            sb.AppendLine("<br />");
    //            sb.AppendLine(orderItem.AttributeDescription);
    //        }
    //        //rental info
    //        if (product.IsRental)
    //        {
    //            var rentalStartDate = orderItem.RentalStartDateUtc.HasValue
    //                ? _productService.FormatRentalDate(product, orderItem.RentalStartDateUtc.Value) : string.Empty;
    //            var rentalEndDate = orderItem.RentalEndDateUtc.HasValue
    //                ? _productService.FormatRentalDate(product, orderItem.RentalEndDateUtc.Value) : string.Empty;
    //            var rentalInfo = string.Format(await _localizationService.GetResourceAsync("Order.Rental.FormattedDate"),
    //                rentalStartDate, rentalEndDate);
    //            sb.AppendLine("<br />");
    //            sb.AppendLine(rentalInfo);
    //        }
    //        //SKU
    //        if (_catalogSettings.ShowSkuOnProductDetailsPage)
    //        {
    //            var sku = await _productService.FormatSkuAsync(product, orderItem.AttributesXml);
    //            if (!string.IsNullOrEmpty(sku))
    //            {
    //                sb.AppendLine("<br />");
    //                sb.AppendLine(string.Format(await _localizationService.GetResourceAsync("Messages.Order.Product(s).SKU", languageId), WebUtility.HtmlEncode(sku)));
    //            }
    //        }

    //        sb.AppendLine("</td>");

    //        string unitPriceStr;
    //        if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
    //        {
    //            //including tax
    //            var unitPriceInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.UnitPriceInclTax, order.CurrencyRate);
    //            unitPriceStr = await _priceFormatter.FormatPriceAsync(unitPriceInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, languageId, true);
    //        }
    //        else
    //        {
    //            //excluding tax
    //            var unitPriceExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.UnitPriceExclTax, order.CurrencyRate);
    //            unitPriceStr = await _priceFormatter.FormatPriceAsync(unitPriceExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, languageId, false);
    //        }

    //        sb.AppendLine($"<td style=\"padding: 0.6em 0.4em;text-align: right;\">{unitPriceStr}</td>");

    //        sb.AppendLine($"<td style=\"padding: 0.6em 0.4em;text-align: center;\">{orderItem.Quantity}</td>");

    //        string priceStr;
    //        if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
    //        {
    //            //including tax
    //            var priceInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.PriceInclTax, order.CurrencyRate);
    //            priceStr = await _priceFormatter.FormatPriceAsync(priceInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, languageId, true);
    //        }
    //        else
    //        {
    //            //excluding tax
    //            var priceExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.PriceExclTax, order.CurrencyRate);
    //            priceStr = await _priceFormatter.FormatPriceAsync(priceExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, languageId, false);
    //        }

    //        sb.AppendLine($"<td style=\"padding: 0.6em 0.4em;text-align: right;\">{priceStr}</td>");

    //        sb.AppendLine("</tr>");
    //    }

    //    if (vendorId == 0)
    //    {
    //        //we render checkout attributes and totals only for store owners (hide for vendors)

    //        if (!string.IsNullOrEmpty(order.CheckoutAttributeDescription))
    //        {
    //            sb.AppendLine("<tr><td style=\"text-align:right;\" colspan=\"1\">&nbsp;</td><td colspan=\"3\" style=\"text-align:right\">");
    //            sb.AppendLine(order.CheckoutAttributeDescription);
    //            sb.AppendLine("</td></tr>");
    //        }

    //        //totals
    //        await WriteTotalsAsync(order, language, sb);
    //    }

    //    sb.AppendLine("</table>");
    //    var result = sb.ToString();
    //    return result;
    //}

    ///// <summary>
    ///// Write order totals
    ///// </summary>
    ///// <param name="order">Order</param>
    ///// <param name="language">Language</param>
    ///// <param name="sb">StringBuilder</param>
    ///// <returns>A task that represents the asynchronous operation</returns>
    //protected virtual async Task WriteTotalsAsync(Order order, Language language, StringBuilder sb)
    //{
    //    //subtotal
    //    string cusSubTotal;
    //    var displaySubTotalDiscount = false;
    //    var cusSubTotalDiscount = string.Empty;
    //    var languageId = language.Id;
    //    if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax && !_taxSettings.ForceTaxExclusionFromOrderSubtotal)
    //    {
    //        //including tax

    //        //subtotal
    //        var orderSubtotalInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubtotalInclTax, order.CurrencyRate);
    //        cusSubTotal = await _priceFormatter.FormatPriceAsync(orderSubtotalInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, languageId, true);
    //        //discount (applied to order subtotal)
    //        var orderSubTotalDiscountInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubTotalDiscountInclTax, order.CurrencyRate);
    //        if (orderSubTotalDiscountInclTaxInCustomerCurrency > decimal.Zero)
    //        {
    //            cusSubTotalDiscount = await _priceFormatter.FormatPriceAsync(-orderSubTotalDiscountInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, languageId, true);
    //            displaySubTotalDiscount = true;
    //        }
    //    }
    //    else
    //    {
    //        //excluding tax

    //        //subtotal
    //        var orderSubtotalExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubtotalExclTax, order.CurrencyRate);
    //        cusSubTotal = await _priceFormatter.FormatPriceAsync(orderSubtotalExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, languageId, false);
    //        //discount (applied to order subtotal)
    //        var orderSubTotalDiscountExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubTotalDiscountExclTax, order.CurrencyRate);
    //        if (orderSubTotalDiscountExclTaxInCustomerCurrency > decimal.Zero)
    //        {
    //            cusSubTotalDiscount = await _priceFormatter.FormatPriceAsync(-orderSubTotalDiscountExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, languageId, false);
    //            displaySubTotalDiscount = true;
    //        }
    //    }

    //    //shipping, payment method fee
    //    string cusShipTotal;
    //    string cusPaymentMethodAdditionalFee;
    //    var taxRates = new SortedDictionary<decimal, decimal>();
    //    var cusTaxTotal = string.Empty;
    //    var cusDiscount = string.Empty;
    //    if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
    //    {
    //        //including tax

    //        //shipping
    //        var orderShippingInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderShippingInclTax, order.CurrencyRate);
    //        cusShipTotal = await _priceFormatter.FormatShippingPriceAsync(orderShippingInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, languageId, true);
    //        //payment method additional fee
    //        var paymentMethodAdditionalFeeInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.PaymentMethodAdditionalFeeInclTax, order.CurrencyRate);
    //        cusPaymentMethodAdditionalFee = await _priceFormatter.FormatPaymentMethodAdditionalFeeAsync(paymentMethodAdditionalFeeInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, languageId, true);
    //    }
    //    else
    //    {
    //        //excluding tax

    //        //shipping
    //        var orderShippingExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderShippingExclTax, order.CurrencyRate);
    //        cusShipTotal = await _priceFormatter.FormatShippingPriceAsync(orderShippingExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, languageId, false);
    //        //payment method additional fee
    //        var paymentMethodAdditionalFeeExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.PaymentMethodAdditionalFeeExclTax, order.CurrencyRate);
    //        cusPaymentMethodAdditionalFee = await _priceFormatter.FormatPaymentMethodAdditionalFeeAsync(paymentMethodAdditionalFeeExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, languageId, false);
    //    }

    //    //shipping
    //    var displayShipping = order.ShippingStatus != ShippingStatus.ShippingNotRequired;

    //    //payment method fee
    //    var displayPaymentMethodFee = order.PaymentMethodAdditionalFeeExclTax > decimal.Zero;

    //    //tax
    //    bool displayTax;
    //    bool displayTaxRates;
    //    if (_taxSettings.HideTaxInOrderSummary && order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
    //    {
    //        displayTax = false;
    //        displayTaxRates = false;
    //    }
    //    else
    //    {
    //        if (order.OrderTax == 0 && _taxSettings.HideZeroTax)
    //        {
    //            displayTax = false;
    //            displayTaxRates = false;
    //        }
    //        else
    //        {
    //            taxRates = new SortedDictionary<decimal, decimal>();
    //            foreach (var tr in _orderService.ParseTaxRates(order, order.TaxRates))
    //                taxRates.Add(tr.Key, _currencyService.ConvertCurrency(tr.Value, order.CurrencyRate));

    //            displayTaxRates = _taxSettings.DisplayTaxRates && taxRates.Any();
    //            displayTax = !displayTaxRates;

    //            var orderTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderTax, order.CurrencyRate);
    //            var taxStr = await _priceFormatter.FormatPriceAsync(orderTaxInCustomerCurrency, true, order.CustomerCurrencyCode,
    //                false, languageId);
    //            cusTaxTotal = taxStr;
    //        }
    //    }

    //    //discount
    //    var displayDiscount = false;
    //    if (order.OrderDiscount > decimal.Zero)
    //    {
    //        var orderDiscountInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderDiscount, order.CurrencyRate);
    //        cusDiscount = await _priceFormatter.FormatPriceAsync(-orderDiscountInCustomerCurrency, true, order.CustomerCurrencyCode, false, languageId);
    //        displayDiscount = true;
    //    }

    //    //total
    //    var orderTotalInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderTotal, order.CurrencyRate);
    //    var cusTotal = await _priceFormatter.FormatPriceAsync(orderTotalInCustomerCurrency, true, order.CustomerCurrencyCode, false, languageId);

    //    //subtotal
    //    sb.AppendLine($"<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{await _localizationService.GetResourceAsync("Messages.Order.SubTotal", languageId)}</strong></td> <td style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{cusSubTotal}</strong></td></tr>");

    //    //discount (applied to order subtotal)
    //    if (displaySubTotalDiscount)
    //    {
    //        sb.AppendLine($"<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{await _localizationService.GetResourceAsync("Messages.Order.SubTotalDiscount", languageId)}</strong></td> <td style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{cusSubTotalDiscount}</strong></td></tr>");
    //    }

    //    //shipping
    //    if (displayShipping)
    //    {
    //        sb.AppendLine($"<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{await _localizationService.GetResourceAsync("Messages.Order.Shipping", languageId)}</strong></td> <td style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{cusShipTotal}</strong></td></tr>");
    //    }

    //    //payment method fee
    //    if (displayPaymentMethodFee)
    //    {
    //        var paymentMethodFeeTitle = await _localizationService.GetResourceAsync("Messages.Order.PaymentMethodAdditionalFee", languageId);
    //        sb.AppendLine($"<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{paymentMethodFeeTitle}</strong></td> <td style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{cusPaymentMethodAdditionalFee}</strong></td></tr>");
    //    }

    //    //tax
    //    if (displayTax)
    //    {
    //        sb.AppendLine($"<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{await _localizationService.GetResourceAsync("Messages.Order.Tax", languageId)}</strong></td> <td style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{cusTaxTotal}</strong></td></tr>");
    //    }

    //    if (displayTaxRates)
    //    {
    //        foreach (var item in taxRates)
    //        {
    //            var taxRate = string.Format(await _localizationService.GetResourceAsync("Messages.Order.TaxRateLine"),
    //                _priceFormatter.FormatTaxRate(item.Key));
    //            var taxValue = await _priceFormatter.FormatPriceAsync(item.Value, true, order.CustomerCurrencyCode, false, languageId);
    //            sb.AppendLine($"<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{taxRate}</strong></td> <td style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{taxValue}</strong></td></tr>");
    //        }
    //    }

    //    //discount
    //    if (displayDiscount)
    //    {
    //        sb.AppendLine($"<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{await _localizationService.GetResourceAsync("Messages.Order.TotalDiscount", languageId)}</strong></td> <td style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{cusDiscount}</strong></td></tr>");
    //    }

    //    //gift cards
    //    foreach (var gcuh in await _giftCardService.GetGiftCardUsageHistoryAsync(order))
    //    {
    //        var giftCardText = string.Format(await _localizationService.GetResourceAsync("Messages.Order.GiftCardInfo", languageId),
    //            WebUtility.HtmlEncode((await _giftCardService.GetGiftCardByIdAsync(gcuh.GiftCardId))?.GiftCardCouponCode));
    //        var giftCardAmount = await _priceFormatter.FormatPriceAsync(-_currencyService.ConvertCurrency(gcuh.UsedValue, order.CurrencyRate), true, order.CustomerCurrencyCode,
    //            false, languageId);
    //        sb.AppendLine($"<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{giftCardText}</strong></td> <td style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{giftCardAmount}</strong></td></tr>");
    //    }

    //    //reward points
    //    if (order.RedeemedRewardPointsEntryId.HasValue && await _rewardPointService.GetRewardPointsHistoryEntryByIdAsync(order.RedeemedRewardPointsEntryId.Value) is RewardPointsHistory redeemedRewardPointsEntry)
    //    {
    //        var rpTitle = string.Format(await _localizationService.GetResourceAsync("Messages.Order.RewardPoints", languageId),
    //            -redeemedRewardPointsEntry.Points);
    //        var rpAmount = await _priceFormatter.FormatPriceAsync(-_currencyService.ConvertCurrency(redeemedRewardPointsEntry.UsedAmount, order.CurrencyRate), true,
    //            order.CustomerCurrencyCode, false, languageId);
    //        sb.AppendLine($"<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{rpTitle}</strong></td> <td style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{rpAmount}</strong></td></tr>");
    //    }

    //    //total
    //    sb.AppendLine($"<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{await _localizationService.GetResourceAsync("Messages.Order.OrderTotal", languageId)}</strong></td> <td style=\"background-color: {_templatesSettings.Color3};padding:0.6em 0.4 em;\"><strong>{cusTotal}</strong></td></tr>");
    //}

    ///// <summary>
    ///// Convert a collection to a HTML table
    ///// </summary>
    ///// <param name="shipment">Shipment</param>
    ///// <param name="languageId">Language identifier</param>
    ///// <returns>
    ///// A task that represents the asynchronous operation
    ///// The task result contains the hTML table of products
    ///// </returns>
    //protected virtual async Task<string> ProductListToHtmlTableAsync(Shipment shipment, int languageId)
    //{
    //    var sb = new StringBuilder();
    //    sb.AppendLine("<table border=\"0\" style=\"width:100%;\">");

    //    sb.AppendLine($"<tr style=\"background-color:{_templatesSettings.Color1};text-align:center;\">");
    //    sb.AppendLine($"<th>{await _localizationService.GetResourceAsync("Messages.Order.Product(s).Name", languageId)}</th>");
    //    sb.AppendLine($"<th>{await _localizationService.GetResourceAsync("Messages.Order.Product(s).Quantity", languageId)}</th>");
    //    sb.AppendLine("</tr>");

    //    var table = await _shipmentService.GetShipmentItemsByShipmentIdAsync(shipment.Id);
    //    for (var i = 0; i <= table.Count - 1; i++)
    //    {
    //        var si = table[i];
    //        var orderItem = await _orderService.GetOrderItemByIdAsync(si.OrderItemId);

    //        if (orderItem == null)
    //            continue;

    //        var product = await _productService.GetProductByIdAsync(orderItem.ProductId);

    //        if (product == null)
    //            continue;

    //        sb.AppendLine($"<tr style=\"background-color: {_templatesSettings.Color2};text-align: center;\">");
    //        //product name
    //        var productName = await _localizationService.GetLocalizedAsync(product, x => x.Name, languageId);

    //        sb.AppendLine("<td style=\"padding: 0.6em 0.4em;text-align: left;\">" + WebUtility.HtmlEncode(productName));

    //        //attributes
    //        if (!string.IsNullOrEmpty(orderItem.AttributeDescription))
    //        {
    //            sb.AppendLine("<br />");
    //            sb.AppendLine(orderItem.AttributeDescription);
    //        }

    //        //rental info
    //        if (product.IsRental)
    //        {
    //            var rentalStartDate = orderItem.RentalStartDateUtc.HasValue
    //                ? _productService.FormatRentalDate(product, orderItem.RentalStartDateUtc.Value) : string.Empty;
    //            var rentalEndDate = orderItem.RentalEndDateUtc.HasValue
    //                ? _productService.FormatRentalDate(product, orderItem.RentalEndDateUtc.Value) : string.Empty;
    //            var rentalInfo = string.Format(await _localizationService.GetResourceAsync("Order.Rental.FormattedDate"),
    //                rentalStartDate, rentalEndDate);
    //            sb.AppendLine("<br />");
    //            sb.AppendLine(rentalInfo);
    //        }

    //        //SKU
    //        if (_catalogSettings.ShowSkuOnProductDetailsPage)
    //        {
    //            var sku = await _productService.FormatSkuAsync(product, orderItem.AttributesXml);
    //            if (!string.IsNullOrEmpty(sku))
    //            {
    //                sb.AppendLine("<br />");
    //                sb.AppendLine(string.Format(await _localizationService.GetResourceAsync("Messages.Order.Product(s).SKU", languageId), WebUtility.HtmlEncode(sku)));
    //            }
    //        }

    //        sb.AppendLine("</td>");

    //        sb.AppendLine($"<td style=\"padding: 0.6em 0.4em;text-align: center;\">{si.Quantity}</td>");

    //        sb.AppendLine("</tr>");
    //    }

    //    sb.AppendLine("</table>");
    //    var result = sb.ToString();
    //    return result;
    //}

    ///// <summary>
    ///// Generates an absolute URL for the specified store, routeName and route values
    ///// </summary>
    ///// <param name="storeId">Store identifier; Pass 0 to load URL of the current store</param>
    ///// <param name="routeName">The name of the route that is used to generate URL</param>
    ///// <param name="routeValues">An object that contains route values</param>
    ///// <returns>
    ///// A task that represents the asynchronous operation
    ///// The task result contains the generated URL
    ///// </returns>
    //protected virtual async Task<string> RouteUrlAsync(int storeId = 0, string routeName = null, object routeValues = null)
    //{
    //    try
    //    {
    //        //try to get a store by the passed identifier
    //        var store = await _storeService.GetStoreByIdAsync(storeId) ?? await _storeContext.GetCurrentStoreAsync()
    //            ?? throw new Exception("No store could be loaded");

    //        //ensure that the store URL is specified
    //        if (string.IsNullOrEmpty(store.Url))
    //            throw new Exception("Store URL cannot be empty");

    //        //generate the relative URL
    //        var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
    //        var url = urlHelper.RouteUrl(routeName, routeValues);

    //        //compose the result
    //        return new Uri(new Uri(store.Url), url).AbsoluteUri;
    //    }
    //    catch (Exception exception)
    //    {
    //        var warning = $"When sending a notification, an error occurred while creating a link for '{routeName}', ensure that URL of the store #{storeId} is correct.";
    //        await _logger.WarningAsync(warning, exception);

    //        return string.Empty;
    //    }
    //}

    #endregion

    #region Methods

    /// <summary>
    /// Add store tokens
    /// </summary>
    /// <param name="tokens">List of already added tokens</param>
    /// <param name="store">Store</param>
    /// <param name="emailAccount">Email account</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public virtual async Task AddSampleTokensAsync(IList<Token> tokens)
    {
        tokens.Add(new Token("Test.Name", "Sample Test"));
    }

    public virtual async Task AddTickectCategoryTokensAsync(IList<Token> tokens, TicketCategory ticketCategory)
    {
        tokens.Add(new Token("TicketCategory.Name", ticketCategory.Name));
    }

    public virtual async Task AddOrderTokensAsync(IList<Token> tokens, Order order)
    {
        tokens.Add(new Token("Order.Id", order.Id));
        tokens.Add(new Token("Order.Name", order.Name));
    }

    public virtual async Task AddProductTokensAsync(IList<Token> tokens, Product product)
    {
        tokens.Add(new Token("Product.Name", product.Name));
        tokens.Add(new Token("Product.Type", ((ProductTypeEnum)product.ProductTypeId).ToString()));
        tokens.Add(new Token("Product.StartDateTime", product.StartDateTime.ToLocalDataTime().ToFormatDateStr()));
    }

    public virtual async Task AddVanueTokensAsync(IList<Token> tokens, Venue venue)
    {
        tokens.Add(new Token("Venue.Name", venue.StadiumName));
        tokens.Add(new Token("Venue.Address", venue.Location));
    }

    public virtual async Task AddOrderProductItemTokensAsync(IList<Token> tokens, OrderProductItem orderProductItem)
    {
        tokens.Add(new Token("OrderProductItem.Seat", orderProductItem.Quantity));
    }

    public virtual async Task AddOrderProductItemDetailTokensAsync(IList<Token> tokens, OrderProductItemDetail orderProductItemDetail)
    {
        //tokens.Add(new Token("OrderProductItem.Seat", orderProductItem.Quantity));
    }

    public virtual async Task AddOrderProductItemDetailQRCodeTokensAsync(IList<Token> tokens, G20.Core.Domain.File file)
    {
        tokens.Add(new Token("OrderItemDetail.ORCodeImageUrl", file.ToGetImageUrl()));
    }


    /// <summary>
    /// Get collection of allowed (supported) message tokens
    /// </summary>
    /// <param name="tokenGroups">Collection of token groups; pass null to get all available tokens</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the collection of allowed message tokens
    /// </returns>
    public virtual async Task<IEnumerable<string>> GetListOfAllowedTokensAsync(IEnumerable<string> tokenGroups = null)
    {
        var additionalTokens = new AdditionalTokensAddedEvent
        {
            TokenGroups = tokenGroups
        };
        //await _eventPublisher.PublishAsync(additionalTokens);

        var allowedTokens = AllowedTokens.Where(x => tokenGroups == null || tokenGroups.Contains(x.Key))
            .SelectMany(x => x.Value).ToList();

        allowedTokens.AddRange(additionalTokens.AdditionalTokens);

        return allowedTokens.Distinct();
    }

    /// <summary>
    /// Get token groups of message template
    /// </summary>
    /// <param name="messageTemplate">Message template</param>
    /// <returns>Collection of token group names</returns>
    public virtual IEnumerable<string> GetTokenGroups(MessageTemplate messageTemplate)
    {
        //groups depend on which tokens are added at the appropriate methods in IWorkflowMessageService
        return messageTemplate.Name switch
        {
            //MessageTemplateSystemNames.CUSTOMER_REGISTERED_STORE_OWNER_NOTIFICATION or
            //    MessageTemplateSystemNames.CUSTOMER_WELCOME_MESSAGE or
            //    MessageTemplateSystemNames.CUSTOMER_EMAIL_VALIDATION_MESSAGE or
            //    MessageTemplateSystemNames.CUSTOMER_EMAIL_REVALIDATION_MESSAGE or
            //    MessageTemplateSystemNames.CUSTOMER_PASSWORD_RECOVERY_MESSAGE or
            //    MessageTemplateSystemNames.DELETE_CUSTOMER_REQUEST_STORE_OWNER_NOTIFICATION => new[] { TokenGroupNames.StoreTokens, TokenGroupNames.CustomerTokens },

            MessageTemplateSystemNames.ORDER_MATCH_NOTIFICATION
            //or
            //    MessageTemplateSystemNames.ORDER_PLACED_STORE_OWNER_NOTIFICATION or
            //    MessageTemplateSystemNames.ORDER_PLACED_AFFILIATE_NOTIFICATION or
            //    MessageTemplateSystemNames.ORDER_PAID_STORE_OWNER_NOTIFICATION or
            //    MessageTemplateSystemNames.ORDER_PAID_CUSTOMER_NOTIFICATION or
            //    MessageTemplateSystemNames.ORDER_PAID_VENDOR_NOTIFICATION or
            //    MessageTemplateSystemNames.ORDER_PAID_AFFILIATE_NOTIFICATION or
            //    MessageTemplateSystemNames.ORDER_PLACED_CUSTOMER_NOTIFICATION or
            //    MessageTemplateSystemNames.ORDER_PROCESSING_CUSTOMER_NOTIFICATION or
            //    MessageTemplateSystemNames.ORDER_COMPLETED_CUSTOMER_NOTIFICATION or
            //    MessageTemplateSystemNames.ORDER_CANCELLED_CUSTOMER_NOTIFICATION 
                => [TokenGroupNames.StoreTokens, TokenGroupNames.OrderTokens, TokenGroupNames.CustomerTokens],

        //MessageTemplateSystemNames.SHIPMENT_SENT_CUSTOMER_NOTIFICATION or
        //MessageTemplateSystemNames.SHIPMENT_READY_FOR_PICKUP_CUSTOMER_NOTIFICATION or
        //MessageTemplateSystemNames.SHIPMENT_DELIVERED_CUSTOMER_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.ShipmentTokens, TokenGroupNames.OrderTokens, TokenGroupNames.CustomerTokens],

        //MessageTemplateSystemNames.ORDER_REFUNDED_STORE_OWNER_NOTIFICATION or
        //MessageTemplateSystemNames.ORDER_REFUNDED_CUSTOMER_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.OrderTokens, TokenGroupNames.RefundedOrderTokens, TokenGroupNames.CustomerTokens],

        //MessageTemplateSystemNames.NEW_ORDER_NOTE_ADDED_CUSTOMER_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.OrderNoteTokens, TokenGroupNames.OrderTokens, TokenGroupNames.CustomerTokens],

        //MessageTemplateSystemNames.RECURRING_PAYMENT_CANCELLED_STORE_OWNER_NOTIFICATION or
        //MessageTemplateSystemNames.RECURRING_PAYMENT_CANCELLED_CUSTOMER_NOTIFICATION or
        //MessageTemplateSystemNames.RECURRING_PAYMENT_FAILED_CUSTOMER_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.OrderTokens, TokenGroupNames.CustomerTokens, TokenGroupNames.RecurringPaymentTokens],

        //MessageTemplateSystemNames.NEWSLETTER_SUBSCRIPTION_ACTIVATION_MESSAGE or
        //MessageTemplateSystemNames.NEWSLETTER_SUBSCRIPTION_DEACTIVATION_MESSAGE => [TokenGroupNames.StoreTokens, TokenGroupNames.SubscriptionTokens],

        //MessageTemplateSystemNames.EMAIL_A_FRIEND_MESSAGE => [TokenGroupNames.StoreTokens, TokenGroupNames.CustomerTokens, TokenGroupNames.ProductTokens, TokenGroupNames.EmailAFriendTokens],
        //MessageTemplateSystemNames.WISHLIST_TO_FRIEND_MESSAGE => [TokenGroupNames.StoreTokens, TokenGroupNames.CustomerTokens, TokenGroupNames.WishlistToFriendTokens],

        //MessageTemplateSystemNames.NEW_RETURN_REQUEST_STORE_OWNER_NOTIFICATION or
        //MessageTemplateSystemNames.NEW_RETURN_REQUEST_CUSTOMER_NOTIFICATION or
        //MessageTemplateSystemNames.RETURN_REQUEST_STATUS_CHANGED_CUSTOMER_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.OrderTokens, TokenGroupNames.CustomerTokens, TokenGroupNames.ReturnRequestTokens],

        //MessageTemplateSystemNames.NEW_FORUM_TOPIC_MESSAGE => [TokenGroupNames.StoreTokens, TokenGroupNames.ForumTopicTokens, TokenGroupNames.ForumTokens, TokenGroupNames.CustomerTokens],
        //MessageTemplateSystemNames.NEW_FORUM_POST_MESSAGE => [TokenGroupNames.StoreTokens, TokenGroupNames.ForumPostTokens, TokenGroupNames.ForumTopicTokens, TokenGroupNames.ForumTokens, TokenGroupNames.CustomerTokens],
        //MessageTemplateSystemNames.PRIVATE_MESSAGE_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.PrivateMessageTokens, TokenGroupNames.CustomerTokens],
        //MessageTemplateSystemNames.NEW_VENDOR_ACCOUNT_APPLY_STORE_OWNER_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.CustomerTokens, TokenGroupNames.VendorTokens],
        //MessageTemplateSystemNames.VENDOR_INFORMATION_CHANGE_STORE_OWNER_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.VendorTokens],
        //MessageTemplateSystemNames.GIFT_CARD_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.GiftCardTokens],

        //MessageTemplateSystemNames.PRODUCT_REVIEW_STORE_OWNER_NOTIFICATION or
        //MessageTemplateSystemNames.PRODUCT_REVIEW_REPLY_CUSTOMER_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.ProductReviewTokens, TokenGroupNames.CustomerTokens],

        //MessageTemplateSystemNames.QUANTITY_BELOW_STORE_OWNER_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.ProductTokens],
        //MessageTemplateSystemNames.QUANTITY_BELOW_ATTRIBUTE_COMBINATION_STORE_OWNER_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.ProductTokens, TokenGroupNames.AttributeCombinationTokens],
        //MessageTemplateSystemNames.NEW_VAT_SUBMITTED_STORE_OWNER_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.CustomerTokens, TokenGroupNames.VatValidation],
        //MessageTemplateSystemNames.BLOG_COMMENT_STORE_OWNER_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.BlogCommentTokens, TokenGroupNames.CustomerTokens],
        //MessageTemplateSystemNames.NEWS_COMMENT_STORE_OWNER_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.NewsCommentTokens, TokenGroupNames.CustomerTokens],
        //MessageTemplateSystemNames.BACK_IN_STOCK_NOTIFICATION => [TokenGroupNames.StoreTokens, TokenGroupNames.CustomerTokens, TokenGroupNames.ProductBackInStockTokens],
        //MessageTemplateSystemNames.CONTACT_US_MESSAGE => [TokenGroupNames.StoreTokens, TokenGroupNames.ContactUs],
        //MessageTemplateSystemNames.CONTACT_VENDOR_MESSAGE => [TokenGroupNames.StoreTokens, TokenGroupNames.ContactVendor],
        _ => [],
        };
    }

    public virtual async Task AddUserTokensAsync(IList<Token> tokens, User user)
    {
        tokens.Add(new Token("User.Name", user.UserName));
        tokens.Add(new Token("User.Email", user.Email));
        tokens.Add(new Token("User.Password", user.Password));
    }
    #endregion
}