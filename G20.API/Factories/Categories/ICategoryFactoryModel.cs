using G20.API.Models.Categories;
using System.Threading.Tasks;

namespace G20.API.Factories.Categories
{
    public interface ICategoryFactoryModel
    {
        Task<CategoryListModel> PrepareCategoryListModelAsync(CategorySearchModel searchModel);
    }
}
