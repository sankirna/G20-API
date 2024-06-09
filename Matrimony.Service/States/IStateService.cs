using Matrimony.Core.Domain;
using Matrimony.Core;
using System.Threading.Tasks;

namespace Matrimony.Service.States
{
    public interface IStateService
    {
        Task<IPagedList<State>> GetStatesAsync(string name, int countryId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<State> GetByIdAsync(int Id);
        Task InsertAsync(State entity);
        Task UpdateAsync(State entity);
        Task DeleteAsync(State entity);
    }
}
