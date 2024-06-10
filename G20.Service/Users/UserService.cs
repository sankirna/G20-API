using G20.Core;
using G20.Core.Domain;
using G20.Data;
using System.Threading.Tasks;

namespace G20.Service.Users
{
    public class UserService : IUserService
    {
        protected readonly IRepository<User> _entityRepository;

        public UserService(IRepository<User> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<User>> GetUsersAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var users = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(u => u.Name.Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return users;
        }

        /// <summary>
        /// Insert a profile
        /// </summary>
        /// <param name="Profile">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<bool> CheckDuplicateAsync(int id, string email)
        {
            return await _entityRepository.Table.AnyAsync(x => x.Id != id && x.Email == email);
        }

        public virtual async Task<User> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(User entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(User entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _entityRepository.DeleteAsync(entity);
        }
    }
}
