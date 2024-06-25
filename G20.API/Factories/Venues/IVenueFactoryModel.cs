using G20.API.Models.Venue;
using G20.API.Models.VenueTicketCategoriesMap;
using G20.Core.Domain;

namespace G20.API.Factories.Venues
{
    public interface IVenueFactoryModel
    {
        Task<VenueListModel> PrepareVenueListModelAsync(VenueSearchModel searchModel);
        Task<VenueModel> PrepareVenueModelAsync(Venue entity, bool isDetail = false);
        Task<List<VenueTicketCategoryMapModel>> PrepareVenueTicketCategoryMapListModelAsync(int venueId);
    }
}
