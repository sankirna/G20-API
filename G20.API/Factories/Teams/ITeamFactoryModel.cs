using G20.API.Models.Categories;
using G20.API.Models.Teams;
using G20.Core.Domain;

namespace G20.API.Factories.Teams
{
    public interface ITeamFactoryModel
    {
        Task<TeamListModel> PrepareTeamListModelAsync(TeamSearchModel searchModel);
        Task<TeamModel> PrepareTeamModelAsync(Team entity, bool isDetail = false);
    }
}
