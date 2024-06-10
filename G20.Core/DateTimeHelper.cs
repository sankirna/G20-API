using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Core
{
    public static class DateTimeHelper
    {
        public static DateTime GetCurrentDataTime()
        {
            return DateTime.Now;
        }

        public static DateTime GetCurrentUTCDataTime()
        {
            return GetCurrentDataTime().ToUniversalTime();
        }

    }
}
