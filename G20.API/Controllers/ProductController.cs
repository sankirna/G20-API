using G20.API.Factories.Media;
using G20.API.Factories.Products;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Products;
using G20.API.Models.Tickets;
using G20.Core;
using G20.Core.Domain;
using G20.Service.Products;
using G20.Service.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace G20.API.Controllers
{
    public class ProductController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IProductFactoryModel _productFactoryModel;
        protected readonly IProductService _productService;
        protected readonly ITicketService _ticketService;
        protected readonly IMediaFactoryModel _mediaFactoryModel;

        public ProductController(IWorkContext workContext
            , IProductFactoryModel productFactoryModel
            , IMediaFactoryModel mediaFactoryModel
            , IProductService productService
            ,ITicketService ticketService)
        {
            _workContext = workContext;
            _productFactoryModel = productFactoryModel;
            _productService = productService;
            _mediaFactoryModel = mediaFactoryModel;
            _ticketService = ticketService;
        }

        #region Private Methods



        #endregion

        [HttpPost]
        public virtual async Task<IActionResult> List(ProductSearchModel searchModel)
        {
            var model = await _productFactoryModel.PrepareProductListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return Error("not found");
            var model = product.ToModel<ProductRequestModel>();
            model.File = await _mediaFactoryModel.GetRequestModelAsync(model.FileId);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(ProductRequestModel model)
        {
            var fileId = await _mediaFactoryModel.AddUpdateFile(model.File);
            var product = model.ToEntity<Product>();
            product.FileId = fileId;
            await _productService.InsertAsync(product);
            //foreach (TicketsModel item in model.ListTickets)
            //{
            //    item.Product = model;
            //    var ticket = item.ToEntity<Ticket>();
            //    await _ticketService.InsertAsync(ticket);
            //}
            
            return Success(product.ToModel<ProductRequestModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(ProductRequestModel model)
        {
            var product = await _productService.GetByIdAsync(model.Id);
            if (product == null)
                return Error("not found");
            var fileId = await _mediaFactoryModel.AddUpdateFile(model.File);
            product = model.ToEntity(product);
            product.FileId = fileId;
            await _productService.UpdateAsync(product);
            return Success(product.ToModel<ProductRequestModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return Error("not found");
            await _productService.DeleteAsync(product);
            return Success(id);
        }
    }
}
