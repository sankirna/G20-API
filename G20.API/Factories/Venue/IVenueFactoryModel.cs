using G20.API.Models.Venue;
using G20.API.Models.VenueTicketCategoriesMap;

namespace G20.API.Factories.Venue
{
    public interface IVenueFactoryModel
    {
        Task<VenueListModel> PrepareVenueListModelAsync(VenueSearchModel searchModel);
        Task<List<VenueTicketCategoryMapModel>> PrepareVenueTicketCategoryMapListModelAsync(int venueId);
    }
}
