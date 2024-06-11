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
        public TeamFactoryModel(ITeamService teamService)
        {
            _teamService = teamService;
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
                    return teamModel;
                });
            });

            return model;
        }
    }
}
