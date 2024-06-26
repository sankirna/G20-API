﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Payments
{
    public enum PaymentMethodType
    {
        /// <summary>
        /// All payment information is entered on the site
        /// </summary>
        Standard = 10,

        /// <summary>
        /// A customer is redirected to a third-party site in order to complete the payment
        /// </summary>
        Redirection = 15,

        /// <summary>
        /// Button
        /// </summary>
        Button = 20,
    }
}
