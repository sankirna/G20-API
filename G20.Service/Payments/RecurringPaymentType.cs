using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Payments
{
    public enum RecurringPaymentType
    {
        /// <summary>
        /// Not supported
        /// </summary>
        NotSupported = 0,

        /// <summary>
        /// Manual
        /// </summary>
        Manual = 10,

        /// <summary>
        /// Automatic (payment is processed on payment gateway site)
        /// </summary>
        Automatic = 20
    }
}
