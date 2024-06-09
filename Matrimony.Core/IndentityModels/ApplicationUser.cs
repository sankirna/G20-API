using Matrimony.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Core.IndentityModels
{
    public class ApplicationUser : IdentityUser<int>
    {
        public RoleEnum Role { get; set; }
    }
}
