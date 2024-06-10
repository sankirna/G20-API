using G20.Core.Domain;
using G20.Core;

namespace G20.Service.Venue
{
    public interface IVenueService
    {
        Task<IPagedList<Core.Domain.Venue>> GetVenueAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Core.Domain.Venue> GetByIdAsync(int id);
        Task InsertAsync(Core.Domain.Venue entity);
        Task UpdateAsync(Core.Domain.Venue entity);
        Task DeleteAsync(Core.Domain.Venue entity);
    }
}
