using G20.Core.Domain;
using G20.Core;

namespace G20.Service.Sponsors
{
    public interface ISponsorService
    {
        Task<IPagedList<Sponsor>> GetSponsorsAsync(string name, int countryId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Sponsor> GetByIdAsync(int Id);
        Task InsertAsync(Sponsor entity);
        Task UpdateAsync(Sponsor entity);
        Task DeleteAsync(Sponsor entity);
    }
}
