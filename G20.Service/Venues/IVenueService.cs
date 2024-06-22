using G20.Core.Domain;
using G20.Core;

namespace G20.Service.Venues
{
    public interface IVenueService
    {
        Task<IPagedList<Venue>> GetVenueAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Venue> GetByIdAsync(int id);
        Task InsertAsync(Venue entity);
        Task UpdateAsync(Venue entity);
        Task DeleteAsync(Venue entity);
    }
}
