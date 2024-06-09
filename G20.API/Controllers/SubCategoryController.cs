using G20.API.Factories.SubCategories;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.SubCategories;
using G20.Core;
using G20.Core.Domain;
using G20.Service.SubCategories;
using Microsoft.AspNetCore.Mvc;

namespace G20.API.Controllers
{
    public class SubCategoryController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly ISubCategoryFactoryModel _subSubCategoryFactoryModel;
        protected readonly ISubCategoryService _subSubCategoryService;

        public SubCategoryController(IWorkContext workContext, ISubCategoryFactoryModel subSubCategoryFactoryModel, ISubCategoryService subSubCategoryService)
        {
            _workContext = workContext;
            _subSubCategoryFactoryModel = subSubCategoryFactoryModel;
            _subSubCategoryService = subSubCategoryService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(SubCategorySearchModel searchModel)
        {
            var model = await _subSubCategoryFactoryModel.PrepareSubCategoryListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var subSubCategory = await _subSubCategoryService.GetByIdAsync(id);
            if (subSubCategory == null)
                return Error("not found");
            return Success(subSubCategory.ToModel<SubCategoryModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(SubCategoryModel model)
        {
            var subSubCategory = model.ToEntity<SubCategory>();
            await _subSubCategoryService.InsertAsync(subSubCategory);
            return Success(subSubCategory.ToModel<SubCategoryModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(SubCategoryModel model)
        {
            var subSubCategory = await _subSubCategoryService.GetByIdAsync(model.Id);
            if (subSubCategory == null)
                return Error("not found");

            subSubCategory = model.ToEntity(subSubCategory);
            await _subSubCategoryService.UpdateAsync(subSubCategory);
            return Success(subSubCategory.ToModel<SubCategoryModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var subSubCategory = await _subSubCategoryService.GetByIdAsync(id);
            if (subSubCategory == null)
                return Error("not found");
            await _subSubCategoryService.DeleteAsync(subSubCategory);
            return Success(id);
        }
    }
}
