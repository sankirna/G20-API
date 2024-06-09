using Matrimony.Core.Domain;
using Matrimony.Core;

namespace Matrimony.Service.Families
{
    public interface IFamilyService
    {
        Task<IPagedList<Family>> GetFamiliesAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<IList<Family>> GetByProfileIdAsync(int profileId);
        Task<Family> GetByIdAsync(int Id);
        Task InsertAsync(Family entity);
        Task UpdateAsync(Family entity);
        Task DeleteAsync(Family entity);
    }
}
