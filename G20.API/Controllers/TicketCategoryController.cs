using G20.API.Factories.TicketCategory;
using G20.API.Models.TicketCategories;
using G20.Core.Domain;
using G20.Core;
using G20.Service.TicketCategories;
using Microsoft.AspNetCore.Mvc;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Media;
using G20.API.Factories.Media;
using G20.API.Models.Teams;

namespace G20.API.Controllers
{
    public class TicketCategoryController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly ITicketCategoryFactoryModel _ticketCategoryFactoryModel;
        protected readonly ITicketCategoryService _ticketCategoryService;
        protected readonly IMediaFactoryModel _mediaFactoryModel;



        public TicketCategoryController(IWorkContext workContext
            , ITicketCategoryFactoryModel ticketCategoryFactoryModel
            , IMediaFactoryModel mediaFactoryModel
            , ITicketCategoryService ticketCategoryService)
        {
            _workContext = workContext;
            _ticketCategoryFactoryModel = ticketCategoryFactoryModel;
            _ticketCategoryService = ticketCategoryService;
            _mediaFactoryModel=mediaFactoryModel;
        }

        #region Private Methods

        

        #endregion

        [HttpPost]
        public virtual async Task<IActionResult> List(TicketCategorySearchModel searchModel)
        {
            var model = await _ticketCategoryFactoryModel.PrepareTicketCategoryListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var ticketCategory = await _ticketCategoryService.GetByIdAsync(id);
            if (ticketCategory == null)
                return Error("not found");
            var model = ticketCategory.ToModel<TicketCategoryModel>();
            model.File = await _mediaFactoryModel.GetRequestModelAsync(model.FileId);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TicketCategoryModel model)
        {
            var fileId = await _mediaFactoryModel.AddUpdateFile(model.File);
            var ticketCategory = model.ToEntity<TicketCategory>();
            ticketCategory.FileId = fileId;
            await _ticketCategoryService.InsertAsync(ticketCategory);
            return Success(ticketCategory.ToModel<TicketCategoryModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(TicketCategoryModel model)
        {
            var ticketCategory = await _ticketCategoryService.GetByIdAsync(model.Id);
            if (ticketCategory == null)
                return Error("not found");
            var fileId = await _mediaFactoryModel.AddUpdateFile(model.File);
            ticketCategory = model.ToEntity(ticketCategory);
            ticketCategory.FileId = fileId;
            await _ticketCategoryService.UpdateAsync(ticketCategory);
            return Success(ticketCategory.ToModel<TicketCategoryModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var ticketCategory = await _ticketCategoryService.GetByIdAsync(id);
            if (ticketCategory == null)
                return Error("not found");
            await _ticketCategoryService.DeleteAsync(ticketCategory);
            return Success(id);
        }
    }
}
