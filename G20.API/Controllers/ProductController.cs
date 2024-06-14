using G20.API.Factories.Media;
using G20.API.Factories.Products;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.ProductCombos;
using G20.API.Models.Products;
using G20.API.Models.ProductTicketCategoriesMap;
using G20.API.Models.Tickets;
using G20.API.Models.VenueTicketCategoriesMap;
using G20.Core;
using G20.Core.Domain;
using G20.Core.Enums;
using G20.Service.ProductCombos;
using G20.Service.Products;
using G20.Service.ProductTicketCategoriesMap;
using G20.Service.Tickets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Nop.Core;

namespace G20.API.Controllers
{
    public class ProductController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IProductFactoryModel _productFactoryModel;
        protected readonly IProductService _productService;
        protected readonly IProductComboService _productComboService;
        protected readonly ITicketService _ticketService;
        protected readonly IMediaFactoryModel _mediaFactoryModel;
        protected readonly IProductTicketCategoryMapService _productTicketCategoryMapService;

        public ProductController(IWorkContext workContext
            , IProductFactoryModel productFactoryModel
            , IMediaFactoryModel mediaFactoryModel
            , IProductService productService
            , IProductComboService productComboService
            , ITicketService ticketService
            , IProductTicketCategoryMapService productTicketCategoryMapService)
        {
            _workContext = workContext;
            _productFactoryModel = productFactoryModel;
            _productService = productService;
            _productComboService = productComboService;
            _mediaFactoryModel = mediaFactoryModel;
            _ticketService = ticketService;
            _productTicketCategoryMapService = productTicketCategoryMapService;
        }

        #region Private Method

        private async Task AddUpdateProductTicketCategoryMapModels(int productId, List<ProductTicketCategoryMapModel> productTicketCategoryMapsModel)
        {
            if (productTicketCategoryMapsModel != null)
            {
                productTicketCategoryMapsModel.ForEach(x => { x.ProductId = productId; });
                var productTicketCategoryMaps = await _productTicketCategoryMapService.GetProductTicketCategoryMapsByProductIdAsync(productId);
                var existingIds = productTicketCategoryMaps.Select(x => x.Id);
                var requestIds = productTicketCategoryMapsModel.Select(x => x.Id);
                var updateIds = requestIds.Intersect(existingIds);
                var deleteIds = existingIds.Except(requestIds);
                var addedIds = requestIds.Except(existingIds);

                var deleteProductTicketCategoryMaps = productTicketCategoryMaps.Where(x => deleteIds.Contains(x.Id));
                foreach (var productTicketCategoryMap in deleteProductTicketCategoryMaps)
                {
                    await _productTicketCategoryMapService.DeleteAsync(productTicketCategoryMap);
                }

                var updateProductTicketCategoryMaps = productTicketCategoryMapsModel.Where(x => updateIds.Contains(x.Id));
                foreach (var productTicketCategoryMapRequest in updateProductTicketCategoryMaps)
                {
                    var productTicketCategoryMap = productTicketCategoryMaps.FirstOrDefault(x => x.Id == productTicketCategoryMapRequest.Id);
                    if (productTicketCategoryMap == null)
                        throw new NopException("product ticket Category Map not found");
                    productTicketCategoryMapRequest.ProductId = productId;
                    productTicketCategoryMap = productTicketCategoryMapRequest.ToEntity(productTicketCategoryMap);
                    await _productTicketCategoryMapService.UpdateAsync(productTicketCategoryMap);
                }

                var addProductTicketCategoryMaps = productTicketCategoryMapsModel.Where(x => addedIds.Contains(x.Id));
                foreach (var productTicketCategoryMapRequest in addProductTicketCategoryMaps)
                {
                    productTicketCategoryMapRequest.ProductId = productId;
                    var productTicketCategoryMap = productTicketCategoryMapRequest.ToEntity<ProductTicketCategoryMap>();
                    await _productTicketCategoryMapService.InsertAsync(productTicketCategoryMap);
                }
            }
        }

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
            if (model.ProductTypeEnum == ProductTypeEnum.Regular && product.VenueId.HasValue)
            {
                model.ProductTicketCategories = await _productFactoryModel.PrepareProductTicketCategoryMapListModelAsync(id, product.VenueId.Value);
            }
            if (model.ProductTypeEnum == ProductTypeEnum.Combo)
            {
                model.ProductCombos = (await _productComboService.GetProductCombosByProductIdAsync(id))
                                     .ToList().Select(c => c.ToModel<ProductComboModel>()).ToList();
            }

            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(ProductRequestModel model)
        {
            var fileId = await _mediaFactoryModel.AddUpdateFile(model.File);
            var product = model.ToEntity<Product>();
            product.FileId = fileId;
            await _productService.InsertAsync(product);

            var entityUpdatedModel = product.ToModel<ProductRequestModel>();
            if (entityUpdatedModel.ProductTypeEnum == ProductTypeEnum.Regular)
            {
                await AddUpdateProductTicketCategoryMapModels(entityUpdatedModel.Id, model.ProductTicketCategories);
            }
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
            var entityUpdatedModel = product.ToModel<ProductRequestModel>();
            if (entityUpdatedModel.ProductTypeEnum == ProductTypeEnum.Regular)
            {
                await AddUpdateProductTicketCategoryMapModels(model.Id, model.ProductTicketCategories);
            }
            return Success(entityUpdatedModel);
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
