using G20.API.Factories.StandCategory;
using G20.API.Models.StandCategories;
using G20.Core.Domain;
using G20.Core;
using G20.Service.StandCategories;
using Microsoft.AspNetCore.Mvc;
using G20.API.Infrastructure.Mapper.Extensions;

namespace G20.API.Controllers
{
    public class StandCategoryController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IStandCategoryFactoryModel _standCategoryFactoryModel;
        protected readonly IStandCategoryService _standCategoryService;


        public StandCategoryController(IWorkContext workContext, IStandCategoryFactoryModel standCategoryFactoryModel, IStandCategoryService standCategoryService)
        {
            _workContext = workContext;
            _standCategoryFactoryModel = standCategoryFactoryModel;
            _standCategoryService = standCategoryService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(StandCategorySearchModel searchModel)
        {
            var model = await _standCategoryFactoryModel.PrepareStandCategoryListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var standCategory = await _standCategoryService.GetByIdAsync(id);
            if (standCategory == null)
                return Error("not found");
            return Success(standCategory.ToModel<StandCategoryModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(StandCategoryModel model)
        {
            var standCategory = model.ToEntity<StandCategory>();
            await _standCategoryService.InsertAsync(standCategory);
            return Success(standCategory.ToModel<StandCategoryModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(StandCategoryModel model)
        {
            var standCategory = await _standCategoryService.GetByIdAsync(model.Id);
            if (standCategory == null)
                return Error("not found");

            standCategory = model.ToEntity(standCategory);
            await _standCategoryService.UpdateAsync(standCategory);
            return Success(standCategory.ToModel<StandCategoryModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var standCategory = await _standCategoryService.GetByIdAsync(id);
            if (standCategory == null)
                return Error("not found");
            await _standCategoryService.DeleteAsync(standCategory);
            return Success(id);
        }
    }
}
