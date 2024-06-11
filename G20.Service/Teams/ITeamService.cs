using G20.Core.Domain;
using G20.Core;
using System.Threading.Tasks;

namespace G20.Service.Teams
{
    public interface ITeamService
    {
        Task<IPagedList<Team>> GetTeamsAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Team> GetByIdAsync(int Id);
        Task InsertAsync(Team entity);
        Task UpdateAsync(Team entity);
        Task DeleteAsync(Team entity);
    }
}
