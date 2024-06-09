using G20.API.Models.SubCategories;
using System.Threading.Tasks;

namespace G20.API.Factories.SubCategories
{
    public interface ISubCategoryFactoryModel
    {
        Task<SubCategoryListModel> PrepareSubCategoryListModelAsync(SubCategorySearchModel searchModel);
    }
}
