using G20.Core.Domain;
using G20.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Orders
{
    public static class OrderExtensions
    {
        public static PaymentTypeEnum ToPaymentType(this Order order)
        {
            return (PaymentTypeEnum)order.PaymentTypeId;
        }
    }
}
