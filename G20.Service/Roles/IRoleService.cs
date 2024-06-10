using G20.Core;
using G20.Core.Domain;
using System.Threading.Tasks;

namespace G20.Service.Roles
{
    public interface IRoleService
    {
        Task<IPagedList<Role>> GetRolesAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Role> GetByIdAsync(int id);
        Task InsertAsync(Role entity);
        Task UpdateAsync(Role entity);
        Task DeleteAsync(Role entity);
    }
}
