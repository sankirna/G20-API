using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Payments.ManualPayment
{
    public enum TransactMode
    {
        /// <summary>
        /// Pending
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Authorize
        /// </summary>
        Authorize = 1,

        /// <summary>
        /// Authorize and capture
        /// </summary>
        AuthorizeAndCapture = 2
    }
}
