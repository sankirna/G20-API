using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.SubCategories;
using G20.Service.Categories;
using G20.Service.SubCategories;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace G20.API.Factories.SubCategories
{
    public class SubCategoryFactoryModel : ISubCategoryFactoryModel
    {
        protected readonly ICategoryService _categoryService;
        protected readonly ISubCategoryService _subCategoryService;

        public SubCategoryFactoryModel(
            ICategoryService categoryService
            , ISubCategoryService subCategoryService)
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }

        public virtual async Task<SubCategoryListModel> PrepareSubCategoryListModelAsync(SubCategorySearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var subCategories = await _subCategoryService.GetSubCategoriesAsync(name: searchModel.Name,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new SubCategoryListModel().PrepareToGridAsync(searchModel, subCategories, () =>
            {
                return subCategories.SelectAwait(async subCategory =>
                {
                    var subCategoryModel = subCategory.ToModel<SubCategoryModel>();
                    var category = await _categoryService.GetByIdAsync(subCategory.CategoryId);
                    subCategoryModel.ParentCategoryName = category?.Name;
                    return subCategoryModel;
                });
            });

            return model;
        }
    }
}
