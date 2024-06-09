using Matrimony.Core.Domain;
using Matrimony.Core;

namespace Matrimony.Service.Cities
{
    public interface ICityService
    {
        Task<IPagedList<City>> GetCitiesAsync(string name, int stateId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<City> GetByIdAsync(int Id);
        Task InsertAsync(City entity);
        Task UpdateAsync(City entity);
        Task DeleteAsync(City entity);
    }
}
