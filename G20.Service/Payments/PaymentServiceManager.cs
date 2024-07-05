using G20.Core.Enums;
using G20.Service.Payments.ManualPayment;
using G20.Service.Venues;
using Nop.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Payments
{
    public class PaymentServiceManager
    {
        public PaymentServiceManager() { }

        public static IPaymentMethod GetPaymentMethod(PaymentTypeEnum paymentType)
        {
            switch (paymentType)
            {
                case PaymentTypeEnum.Cash:
                    return EngineContext.Current.Resolve<CashPaymentProcessor>();
                    break;
                case PaymentTypeEnum.Strip:
                    break;
                default:
                    break;
            }

            return null;
        }
    }
}
