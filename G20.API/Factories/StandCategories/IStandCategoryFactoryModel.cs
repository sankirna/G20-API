using G20.API.Models.StandCategories;

namespace G20.API.Factories.StandCategory
{
    public interface IStandCategoryFactoryModel
    {
        Task<StandCategoryListModel> PrepareStandCategoryListModelAsync(StandCategorySearchModel searchModel);
    }
}
