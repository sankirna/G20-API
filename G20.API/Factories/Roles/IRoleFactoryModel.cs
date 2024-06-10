using G20.API.Models.Roles;
using System.Threading.Tasks;

namespace G20.API.Factories.Roles
{
    public interface IRoleFactoryModel
    {
        Task<RoleListModel> PrepareRoleListModelAsync(RoleSearchModel searchModel);
    }
}
