using G20.API.Models.Venue;

namespace G20.API.Factories.Venue
{
    public interface IVenueFactoryModel
    {
        Task<VenueListModel> PrepareVenueListModelAsync(VenueSearchModel searchModel);
    }
}
