using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Roles;
using G20.Service.Roles;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Factories.Roles
{
    public class RoleFactoryModel : IRoleFactoryModel
    {
        protected readonly IRoleService _roleService;

        public RoleFactoryModel(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public virtual async Task<RoleListModel> PrepareRoleListModelAsync(RoleSearchModel searchModel)
        {
            var roles = await _roleService.GetRolesAsync(searchModel.Name, searchModel.Page - 1, searchModel.PageSize);
            var model = await new RoleListModel().PrepareToGridAsync(searchModel, roles, () =>
            {
                return roles.SelectAwait(async role =>
                {
                    var roleModel = role.ToModel<RoleModel>();
                    return roleModel;
                });
            });

            return model;
        }
    }
}
