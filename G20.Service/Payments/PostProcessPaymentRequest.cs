using G20.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Payments
{
    public partial class PostProcessPaymentRequest
    {
        /// <summary>
        /// Gets or sets an order. Used when order is already saved (payment gateways that redirect a customer to a third-party URL)
        /// </summary>
        public Order Order { get; set; }

        public int OrderId { get;set; }
    }
}
