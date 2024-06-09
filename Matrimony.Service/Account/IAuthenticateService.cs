using Matrimony.Core.IndentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Service.Account
{
    public interface IAuthenticateService
    {
        Task<ApplicationUser> FindByNameAsync(string username);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<bool> CreateAsync(ApplicationUser user, string password);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<bool> RoleExistsAsync(string roleName);
        Task<bool> CreateAsync(string roleName);
        Task<bool> AddToRoleAsync(ApplicationUser user, string role);
    }
}
