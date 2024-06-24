using G20.API.Models.Users;
using G20.Core.Domain;
using System.Threading.Tasks;

namespace G20.API.Factories.Users
{
    public interface IUserFactoryModel
    {
        Task<UserListModel> PrepareUserListModelAsync(UserSearchModel searchModel);
        Task<UserModel> PrepareUserModelAsync(User entity, bool isDetail = false);
    }
}
