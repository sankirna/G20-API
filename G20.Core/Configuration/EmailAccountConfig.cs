using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Core.Configuration
{
    public partial class  EmailAccountConfig : IConfig
    {
        public int DefualtEmailAccountId { get; set; }
    }
}
