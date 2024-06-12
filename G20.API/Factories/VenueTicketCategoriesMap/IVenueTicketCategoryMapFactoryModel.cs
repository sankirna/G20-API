using G20.API.Models.VenueTicketCategoriesMap;

namespace G20.API.Factories.VenueTicketCategoriesMap
{
    public interface IVenueTicketCategoryMapFactoryModel
    {
        Task<VenueTicketCategoryMapListModel> PrepareVenueTicketCategoryMapListModelAsync(VenueTicketCategoryMapSearchModel searchModel);
    }
}
