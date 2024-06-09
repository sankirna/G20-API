using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.SubCategories;
using G20.Service.SubCategories;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace G20.API.Factories.SubCategories
{
    public class SubCategoryFactoryModel : ISubCategoryFactoryModel
    {
        protected readonly ISubCategoryService _subSubCategoryService;

        public SubCategoryFactoryModel(ISubCategoryService subSubCategoryService)
        {
            _subSubCategoryService = subSubCategoryService;
        }

        public virtual async Task<SubCategoryListModel> PrepareSubCategoryListModelAsync(SubCategorySearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var subCategories = await _subSubCategoryService.GetSubCategoriesAsync(name: searchModel.Name,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new SubCategoryListModel().PrepareToGridAsync(searchModel, subCategories, () =>
            {
                return subCategories.SelectAwait(async subSubCategory =>
                {
                    var subSubCategoryModel = subSubCategory.ToModel<SubCategoryModel>();
                    return subSubCategoryModel;
                });
            });

            return model;
        }
    }
}
