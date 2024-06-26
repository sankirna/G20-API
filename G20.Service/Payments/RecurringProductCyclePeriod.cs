using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Payments
{
    public enum RecurringProductCyclePeriod
    {
        /// <summary>
        /// Days
        /// </summary>
        Days = 0,

        /// <summary>
        /// Weeks
        /// </summary>
        Weeks = 10,

        /// <summary>
        /// Months
        /// </summary>
        Months = 20,

        /// <summary>
        /// Years
        /// </summary>
        Years = 30,
    }
}
