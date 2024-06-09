using Matrimony.Core.Domain;
using Matrimony.Core;

namespace Matrimony.Service.Educations
{
    public interface IEducationService
    {
        Task<IPagedList<Education>> GetEducationsAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<IList<Education>> GetByProfileIdAsync(int profileId);
        Task<Education> GetByIdAsync(int Id);
        Task InsertAsync(Education entity);
        Task UpdateAsync(Education entity);
        Task DeleteAsync(Education entity);
    }
}
