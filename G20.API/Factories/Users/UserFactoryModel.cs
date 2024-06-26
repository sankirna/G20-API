﻿using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Teams;
using G20.API.Models.Users;
using G20.Core.Domain;
using G20.Service.Users;
using Nop.Web.Framework.Models.Extensions;
using System.Threading.Tasks;

namespace G20.API.Factories.Users
{
    public class UserFactoryModel : IUserFactoryModel
    {
        protected readonly IUserService _userService;

        public UserFactoryModel(IUserService userService)
        {
            _userService = userService;
        }

        public virtual async Task<UserListModel> PrepareUserListModelAsync(UserSearchModel searchModel)
        {
            var users = await _userService.GetUsersAsync(searchModel.Name, searchModel.Page - 1, searchModel.PageSize);
            var model = await new UserListModel().PrepareToGridAsync(searchModel, users, () =>
            {
                return users.SelectAwait(async user =>
                {
                    var userModel = await PrepareUserModelAsync(user);
                    return userModel;
                });
            });

            return model;
        }

        public virtual async Task<UserModel> PrepareUserModelAsync(User entity, bool isDetail = false)
        {
            var model = entity.ToModel<UserModel>();
            return model;
        }
    }
}
