using G20.Core.Domain;
using G20.Core;

namespace G20.Service.Educations
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
