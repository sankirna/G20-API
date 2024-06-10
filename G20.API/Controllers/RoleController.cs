using G20.API.Factories.Roles;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Roles;
using G20.Core;
using G20.Core.Domain;
using G20.Service.Roles;
using Microsoft.AspNetCore.Mvc;

namespace G20.API.Controllers
{
    public class RoleController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IRoleFactoryModel _roleFactoryModel;
        protected readonly IRoleService _roleService;

        public RoleController(IWorkContext workContext, IRoleFactoryModel roleFactoryModel, IRoleService roleService)
        {
            _workContext = workContext;
            _roleFactoryModel = roleFactoryModel;
            _roleService = roleService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(RoleSearchModel searchModel)
        {
            var model = await _roleFactoryModel.PrepareRoleListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
                return Error("not found");
            return Success(role.ToModel<RoleModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(RoleModel model)
        {
            var role = model.ToEntity<Role>();
            await _roleService.InsertAsync(role);
            return Success(role.ToModel<RoleModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(RoleModel model)
        {
            var role = await _roleService.GetByIdAsync(model.Id);
            if (role == null)
                return Error("not found");

            role = model.ToEntity(role);
            await _roleService.UpdateAsync(role);
            return Success(role.ToModel<RoleModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
                return Error("not found");
            await _roleService.DeleteAsync(role);
            return Success(id);
        }
    }
}
