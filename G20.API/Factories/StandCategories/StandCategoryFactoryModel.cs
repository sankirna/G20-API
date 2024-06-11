using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.StandCategories;
using G20.Service.StandCategories;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Factories.StandCategory
{
    public class StandCategoryFactoryModel : IStandCategoryFactoryModel
    {
        protected readonly IStandCategoryService _standCategoryService;

        public StandCategoryFactoryModel(IStandCategoryService standCategoryService)
        {
            _standCategoryService = standCategoryService;
        }

        public virtual async Task<StandCategoryListModel> PrepareStandCategoryListModelAsync(StandCategorySearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var standCategorys = await _standCategoryService.GetStandCategoryAsync(name: searchModel.StandName,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new StandCategoryListModel().PrepareToGridAsync(searchModel, standCategorys, () =>
            {
                return standCategorys.SelectAwait(async standCategory =>
                {
                    var standCategoryModel = standCategory.ToModel<StandCategoryModel>();
                    return standCategoryModel;
                });
            });

            return model;
        }
    }
}
