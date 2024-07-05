using G20.Service.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Orders
{
    public interface IOrderProcessingService
    {
        Task<PlaceOrderResult> PlaceOrderAsync(ProcessPaymentRequest processPaymentRequest);
    }
}
