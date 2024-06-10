using G20.Core;
using G20.Core.Domain;
using G20.Data;
using System.Threading.Tasks;

namespace G20.Service.UserRoles
{
    public class UserRoleService : IUserRoleService
    {
        protected readonly IRepository<UserRole> _entityRepository;
        protected readonly IRepository<Role> _roleRepository;

        public UserRoleService(IRepository<UserRole> entityRepository
            , IRepository<Role> roleRepository)
        {
            _entityRepository = entityRepository;
            _roleRepository = roleRepository;
        }

        public virtual async Task<IPagedList<UserRole>> GetUserRolesAsync( int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var userRoles = await _entityRepository.GetAllPagedAsync(query =>
            {
                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return userRoles;
        }


        public virtual async Task<List<Role>> GetRoleByUserIdAsync(int userId)
        {
            var query = from ur in _entityRepository.Table
                        join r in _roleRepository.Table on ur.RoleId equals r.Id
                        where ur.UserId == userId 
                        select r;

            return await query.ToListAsync();
        }

        public virtual async Task<UserRole> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(UserRole entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task InsertByUserANDRoleIdAsync(int userId, int roleId)
        {
            UserRole entity = new UserRole();
            entity.UserId = userId;
            entity.RoleId = roleId;
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(UserRole entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(UserRole entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _entityRepository.DeleteAsync(entity);
        }

        public virtual async Task DeleteByUserANDRoleIdAsync(int userId,int roleId)
        {
            var userRoles = await _entityRepository.Table.Where(x => x.UserId == userId && x.RoleId == roleId).ToListAsync();

            foreach (var userRole in userRoles)
            {
                await DeleteAsync(userRole);
            }

        }

        public virtual async Task DeleteByUserIdAsync(int userId)
        {
             var  userRoles = await _entityRepository.Table.Where(x=>x.UserId == userId).ToListAsync();

            foreach (var userRole in userRoles)
            {
                await DeleteAsync(userRole);
            }

        }
    }
}
