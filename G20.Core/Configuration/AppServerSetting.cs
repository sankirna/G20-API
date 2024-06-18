using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Core.Configuration
{
    public partial class AppServerSetting: IConfig
    {
        public string BaseURL { get; set; }
    }
}
