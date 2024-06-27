using G20.Service.Payments.ManualPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Payments
{
    public class PaymentServiceManager
    {
        public static IPaymentMethod GetPaymentMethod(string type)
        {
            return new ManualPaymentProcessor();
        }
    }
}
