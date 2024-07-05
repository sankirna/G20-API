using G20.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Orders
{
    public partial class PlaceOrderResult
    {
        public PlaceOrderResult()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Gets a value indicating whether request has been completed successfully
        /// </summary>
        public bool Success => !Errors.Any();

        /// <summary>
        /// Add error
        /// </summary>
        /// <param name="error">Error</param>
        public void AddError(string error)
        {
            Errors.Add(error);
        }

        /// <summary>
        /// Errors
        /// </summary>
        public IList<string> Errors { get; set; }

        /// <summary>
        /// Gets or sets the placed order
        /// </summary>
        public Order PlacedOrder { get; set; }
        public int OrderId { get; set; }
    }
}
