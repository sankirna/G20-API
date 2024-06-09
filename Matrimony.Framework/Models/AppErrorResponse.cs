using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Framework.Models
{
    public class AppErrorResponse
    {
        public List<string> Errors { get; set; }
        public List<string> InnerErrors { get; set; }
    }
}
