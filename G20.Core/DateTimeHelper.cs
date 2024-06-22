using Microsoft.VisualBasic;
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
            return GetCurrentDataTime().ToUTCDataTime();
        }
        public static DateTime ToUTCDataTime(this DateTime dt)
        {
            return dt.ToUniversalTime();
        }

        public static DateTime? ToUTCDataTime(this DateTime? dt)
        {
            return dt.HasValue ? dt.Value.ToUTCDataTime() : dt;
        }

        public static DateTime ToLocalDataTime(this DateTime dt)
        {
            return dt.ToLocalTime();
        }

        public static DateTime? ToLocalDataTime(this DateTime? dt)
        {
            return dt.HasValue ? dt.Value.ToLocalDataTime() : dt;
        }

        public static string ToFormatDateStr(this DateTime? dt)
        {
            return dt.HasValue ? dt.Value.ToFormatDateStr() : "";
        }

        public static string ToFormatDateStr(this DateTime dt)
        {
            return dt.ToString("MMMM dd, yyyy hh:mm tt");
        }
    }
}
