using G20.API.Models.Users;
using System.Threading.Tasks;

namespace G20.API.Factories.Users
{
    public interface IUserFactoryModel
    {
        Task<UserListModel> PrepareUserListModelAsync(UserSearchModel searchModel);
    }
}
