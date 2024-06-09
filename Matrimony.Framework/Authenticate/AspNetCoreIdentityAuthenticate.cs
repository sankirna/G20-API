using Matrimony.Core.IndentityModels;
using Matrimony.Service.Account;
using Microsoft.AspNetCore.Identity;

namespace Matrimony.Framework.Authenticate
{
    public class AspNetCoreIdentityAuthenticate : IAuthenticateService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AspNetCoreIdentityAuthenticate(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> CreateAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<ApplicationUser> FindByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<bool> CreateAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(new ApplicationRole(roleName));
            return result.Succeeded;
        }

        public  async Task<bool> AddToRoleAsync(ApplicationUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }
    }
}
