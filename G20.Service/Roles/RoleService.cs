using G20.Core;
using G20.Core.Domain;
using G20.Data;
using G20.Service.Files;
using System.Threading.Tasks;

namespace G20.Service.Roles
{
    public class RoleService : IRoleService
    {
        protected readonly IRepository<Role> _entityRepository;

        public RoleService(IRepository<Role> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<Role>> GetRolesAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var roles = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(r => r.Name.Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return roles;
        }

        public virtual async Task<Role> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(Role entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(Role entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Role entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _entityRepository.DeleteAsync(entity);
        }
    }
}
