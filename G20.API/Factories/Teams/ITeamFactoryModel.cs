using G20.API.Models.Teams;

namespace G20.API.Factories.Teams
{
    public interface ITeamFactoryModel
    {
        Task<TeamListModel> PrepareTeamListModelAsync(TeamSearchModel searchModel);
    }
}
