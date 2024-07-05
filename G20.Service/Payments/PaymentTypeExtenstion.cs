using G20.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Payments
{
    public static class PaymentTypeExtenstion
    {
        public static PaymentTypeEnum ToPaymentType(this string paymentMethodSystemName)
        {
            var result = Enum.Parse<PaymentTypeEnum>(paymentMethodSystemName);
            return result;
        }
    }
}
