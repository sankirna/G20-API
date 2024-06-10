using G20.API.Factories.Users;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Users;
using G20.Core;
using G20.Core.Domain;
using G20.Service.Users;
using Microsoft.AspNetCore.Mvc;

namespace G20.API.Controllers
{
    public class UserController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IUserFactoryModel _userFactoryModel;
        protected readonly IUserService _userService;

        public UserController(IWorkContext workContext, IUserFactoryModel userFactoryModel, IUserService userService)
        {
            _workContext = workContext;
            _userFactoryModel = userFactoryModel;
            _userService = userService;
        }

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
            return Success(user.ToModel<UserModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(UserModel model)
        {
            var user = model.ToEntity<User>();
            await _userService.InsertAsync(user);
            return Success(user.ToModel<UserModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(UserModel model)
        {
            var user = await _userService.GetByIdAsync(model.Id);
            if (user == null)
                return Error("not found");

            user = model.ToEntity(user);
            await _userService.UpdateAsync(user);
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
