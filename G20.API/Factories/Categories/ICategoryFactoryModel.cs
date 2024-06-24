using G20.API.Models.Categories;
using G20.Core.Domain;
using System.Threading.Tasks;

namespace G20.API.Factories.Categories
{
    public interface ICategoryFactoryModel
    {
        Task<CategoryListModel> PrepareCategoryListModelAsync(CategorySearchModel searchModel);
        Task<CategoryModel> PrepareCategoryModelAsync(Category entity, bool isDetail = false);
    }


}
