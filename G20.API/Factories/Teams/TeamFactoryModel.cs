﻿using G20.API.Factories.Media;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Teams;
using G20.Service.Teams;
using Nop.Web.Framework.Models.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace G20.API.Factories.Teams
{
    public class TeamFactoryModel : ITeamFactoryModel
    {
        protected readonly ITeamService _teamService;
        protected readonly IMediaFactoryModel _mediaFactoryModel;

        public TeamFactoryModel(ITeamService teamService,
            IMediaFactoryModel mediaFactoryModel
            )
        {
            _teamService = teamService;
            _mediaFactoryModel = mediaFactoryModel;
        }

        public virtual async Task<TeamListModel> PrepareTeamListModelAsync(TeamSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var teams = await _teamService.GetTeamsAsync(name: searchModel.Name,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new TeamListModel().PrepareToGridAsync(searchModel, teams, () =>
            {
                return teams.SelectAwait(async team =>
                {
                    var teamModel = team.ToModel<TeamModel>();
                    teamModel.Logo = await _mediaFactoryModel.GetRequestModelAsync(teamModel.logoId);
                    return teamModel;
                });
            });

            return model;
        }
    }
}
