using G20.API.Models.TicketCategories;

namespace G20.API.Factories.TicketCategory
{
    public interface ITicketCategoryFactoryModel
    {
        Task<TicketCategoryListModel> PrepareTicketCategoryListModelAsync(TicketCategorySearchModel searchModel);
    }
}
