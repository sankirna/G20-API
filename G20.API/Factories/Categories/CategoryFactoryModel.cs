using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Categories;
using G20.Service.Categories;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace G20.API.Factories.Categories
{
    public class CategoryFactoryModel : ICategoryFactoryModel
    {
        protected readonly ICategoryService _categoryService;

        public CategoryFactoryModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public virtual async Task<CategoryListModel> PrepareCategoryListModelAsync(CategorySearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var categories = await _categoryService.GetCategoriesAsync(name: searchModel.Name,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new CategoryListModel().PrepareToGridAsync(searchModel, categories, () =>
            {
                return categories.SelectAwait(async category =>
                {
                    var categoryModel = category.ToModel<CategoryModel>();
                    return categoryModel;
                });
            });

            return model;
        }
    }
}
