using AutoMapper;
using G20.API.Auth;
using G20.API.Factories.Users;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Users;
using G20.Core;
using G20.Core.Domain;
using G20.Service.Messages;
using G20.Service.UserRoles;
using G20.Service.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Nop.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace G20.API.Controllers
{
    public class UserController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IUserFactoryModel _userFactoryModel;
        protected readonly IUserService _userService;
        protected readonly IUserRoleService _userRoleService;
        protected readonly IWorkflowMessageService _workflowMessageService;


        public UserController(IWorkContext workContext
            , IUserFactoryModel userFactoryModel
            , IUserService userService
            , IUserRoleService userRoleService
            , IWorkflowMessageService workflowMessageService)
        {
            _workContext = workContext;
            _userFactoryModel = userFactoryModel;
            _userService = userService;
            _userRoleService = userRoleService;
            _workflowMessageService = workflowMessageService;
        }

        #region Private Methods

        private async Task AddOrUpdateUserRoles(int userId, List<int> roleIds)
        {
            if (roleIds != null && roleIds.Any())
            {
                var userRoles = await _userRoleService.GetRoleByUserIdAsync(userId);
                var existingIds = userRoles.Select(x => x.Id);
                var requestIds = roleIds;
                var updateIds = requestIds.Intersect(existingIds);
                var deleteIds = existingIds.Except(requestIds);
                var addedIds = requestIds.Except(existingIds);

                var deleteUserRoles = userRoles.Where(x => deleteIds.Contains(x.Id));
                foreach (var userRole in deleteUserRoles)
                {
                    await _userRoleService.DeleteByUserANDRoleIdAsync(userId, userRole.Id);
                }

                var addUserRoles = requestIds.Where(x => addedIds.Contains(x));
                foreach (var roleId in addUserRoles)
                {
                    await _userRoleService.InsertByUserANDRoleIdAsync(userId, roleId);
                }
            }
        }


        #endregion

        [HttpPost]
        public virtual async Task<IActionResult> List(UserSearchModel searchModel)
        {
            var model = await _userFactoryModel.PrepareUserListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return Error("not found");
            var model = await _userFactoryModel.PrepareUserModelAsync(user);
            model.RoleIds = (await _userRoleService.GetRoleByUserIdAsync(user.Id)).Select(x => x.Id).ToList();
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(UserModel model)
        {
            // check existing profile exist or not
            var isExists = await _userService.CheckDuplicateAsync(0, model.Email);
            if (isExists)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User Profile is already exist." });
            }

            var user = model.ToEntity<User>();
            await _userService.InsertAsync(user);
            await AddOrUpdateUserRoles(user.Id, model.RoleIds);
            await _workflowMessageService.SendTestNotificationMessageAsync();
            return Success(user.ToModel<UserModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(UserModel model)
        {
            var user = await _userService.GetByIdAsync(model.Id);
            if (user == null)
                return Error("not found");

            // check existing profile exist or not
            var isExists = await _userService.CheckDuplicateAsync(model.Id, model.Email);
            if (isExists)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User Profile is already exist." });
            }

            string email = user.Email;

            user = model.ToEntity(user);
            user.Email = email;
            user.UserTypeId=model.UserTypeId;
            await _userService.UpdateAsync(user);
            await AddOrUpdateUserRoles(user.Id, model.RoleIds);
            return Success(user.ToModel<UserModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return Error("not found");
            await _userService.DeleteAsync(user);
            return Success(id);
        }
    }
}
