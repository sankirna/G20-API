using Matrimony.Core.Domain;
using Matrimony.Core;

namespace Matrimony.Service.Occupations
{
    public interface IOccupationService
    {
        Task<IPagedList<Occupation>> GetOccupationsAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<IList< Occupation>> GetByProfileIdAsync(int profileId); 
        Task<Occupation> GetByIdAsync(int Id);
        Task InsertAsync(Occupation entity);
        Task UpdateAsync(Occupation entity);
        Task DeleteAsync(Occupation entity);
    }
}
