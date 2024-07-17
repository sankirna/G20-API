using G20.Core;
using G20.Core.Domain;
using System.Threading.Tasks;

namespace G20.Service.Users
{
    public interface IUserService
    {
        Task<IPagedList<User>> GetUsersAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<bool> CheckDuplicateAsync(int id, string email);
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAndPasswordAsync(string email, string password);
        Task InsertAsync(User entity);
        Task UpdateAsync(User entity);
        Task DeleteAsync(User entity);
        Task<User> GetByEmailAsync(string email);
        Task<bool> SendResetPAsswordNotifications(User user);
    }
}
