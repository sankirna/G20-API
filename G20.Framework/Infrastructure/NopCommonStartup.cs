using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Web.Framework.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Framework.Infrastructure
{
    public partial class NopCommonStartup : INopStartup
    {
        public void Configure(IApplicationBuilder application)
        {
           // throw new NotImplementedException();
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //add default HTTP clients
            services.AddNopHttpClients();
        }

        public int Order => 100;
    }
}
