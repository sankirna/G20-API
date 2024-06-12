using G20.Core.Domain;
using G20.Core;


namespace G20.Service.VenueTicketCategoriesMap
{
    public interface IVenueTicketCategoryMapService
    {
        Task<IPagedList<VenueTicketCategoryMap>> GetVenueTicketCategoryMapsAsync(int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<VenueTicketCategoryMap> GetByIdAsync(int Id);
        Task InsertAsync(VenueTicketCategoryMap entity);
        Task UpdateAsync(VenueTicketCategoryMap entity);
        Task DeleteAsync(VenueTicketCategoryMap entity);
    }
}
