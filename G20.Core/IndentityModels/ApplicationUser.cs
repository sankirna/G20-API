using G20.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Core.IndentityModels
{
    public class ApplicationUser : IdentityUser<int>
    {
        public RoleEnum Role { get; set; }
    }
}
