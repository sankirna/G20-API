using G20.API.Factories.Categories;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Categories;
using G20.Core;
using G20.Core.Domain;
using G20.Service.Categories;
using Microsoft.AspNetCore.Mvc;

namespace G20.API.Controllers
{
    public class CategoryController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly ICategoryFactoryModel _categoryFactoryModel;
        protected readonly ICategoryService _categoryService;

        public CategoryController(IWorkContext workContext, ICategoryFactoryModel categoryFactoryModel, ICategoryService categoryService)
        {
            _workContext = workContext;
            _categoryFactoryModel = categoryFactoryModel;
            _categoryService = categoryService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(CategorySearchModel searchModel)
        {
            var model = await _categoryFactoryModel.PrepareCategoryListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return Error("not found");
            return Success(category.ToModel<CategoryModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(CategoryModel model)
        {
            var category = model.ToEntity<Category>();
            await _categoryService.InsertAsync(category);
            return Success(await _categoryFactoryModel.PrepareCategoryModelAsync(category));
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(CategoryModel model)
        {
            var category = await _categoryService.GetByIdAsync(model.Id);
            if (category == null)
                return Error("not found");

            category = model.ToEntity(category);
            await _categoryService.UpdateAsync(category);
            return Success(category.ToModel<CategoryModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return Error("not found");
            await _categoryService.DeleteAsync(category);
            return Success(id);
        }
    }
}
