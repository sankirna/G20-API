using G20.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Core.Security
{
    public partial class ProxySettings : ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether we should use proxy connection
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the address of the proxy server
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the port of the proxy server
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// Gets or sets the username for proxy connection
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password for proxy connection
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to bypass the proxy server for local addresses
        /// </summary>
        public bool BypassOnLocal { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the handler sends an Authorization header with the request
        /// </summary>
        public bool PreAuthenticate { get; set; }
    }
}
