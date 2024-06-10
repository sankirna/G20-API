using G20.Core;
using G20.Core.Domain;

namespace G20.Service.UserRoles
{
    public interface IUserRoleService
    {
        Task<IPagedList<UserRole>> GetUserRolesAsync(int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<List<Role>> GetRoleByUserIdAsync(int userId);
        Task<UserRole> GetByIdAsync(int id);
        Task InsertAsync(UserRole entity);
        Task UpdateAsync(UserRole entity);
        Task DeleteAsync(UserRole entity);
        Task DeleteByUserIdAsync(int userId);
    }
}
