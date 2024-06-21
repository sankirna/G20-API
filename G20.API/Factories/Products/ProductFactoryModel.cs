﻿using G20.API.Factories.Media;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Products;
using G20.API.Models.ProductTicketCategoriesMap;
using G20.API.Models.VenueTicketCategoriesMap;
using G20.Core.Domain;
using G20.Service.Categories;
using G20.Service.Countries;
using G20.Service.Products;
using G20.Service.ProductTicketCategoriesMap;
using G20.Service.Teams;
using G20.Service.TicketCategories;
using G20.Service.Venue;
using G20.Service.VenueTicketCategoriesMap;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Factories.Products
{
    public class ProductFactoryModel : IProductFactoryModel
    {
        protected readonly IProductService _productService;
        protected readonly IVenueService _venueService;
        protected readonly ICountryService _countryService;
        protected readonly IVenueTicketCategoryMapService _venueTicketCategoryMapService;
        protected readonly IMediaFactoryModel _mediaFactoryModel;
        protected readonly ITicketCategoryService _ticketCategoryService;
        protected readonly ITeamService _teamService;
        protected readonly IProductTicketCategoryMapService _productTicketCategoryMapService;
        protected readonly ICategoryService _categoryService;

        public ProductFactoryModel(
              IProductService productService
            , IVenueService venueService
            , ICountryService countryService
            , IVenueTicketCategoryMapService venueTicketCategoryMapService
            , IMediaFactoryModel mediaFactoryModel
            , ITicketCategoryService ticketCategoryService
            , IProductTicketCategoryMapService productTicketCategoryMapService
            , ITeamService teamService
            ,ICategoryService categoryService
            )
        {
            _productService = productService;
            _venueService = venueService;
            _countryService = countryService;
            _venueTicketCategoryMapService = venueTicketCategoryMapService;
            _mediaFactoryModel = mediaFactoryModel;
            _ticketCategoryService = ticketCategoryService;
            _productTicketCategoryMapService = productTicketCategoryMapService;
            _teamService = teamService;
            _categoryService = categoryService;
        }


        public virtual async Task<ProductListModel> PrepareProductListModelAsync(ProductSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var products = await _productService.GetProductsAsync(name: searchModel.Team, productTypeId: searchModel.ProductTypeId,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new ProductListModel().PrepareToGridAsync(searchModel, products, () =>
            {
                return products.SelectAwait(async product =>
                {
                    if (product.Team1Id != null)
                        product.Team1 = _teamService.GetByIdAsync((int)product.Team1Id).Result;
                    if (product.Team2Id != null)
                        product.Team2 = _teamService.GetByIdAsync((int)product.Team2Id).Result;
                    var productModel = product.ToModel<ProductModel>();
                    if (product.VenueId != null)
                        productModel.VenueName = _venueService.GetByIdAsync((int)product.VenueId).Result.StadiumName;
                    if (product.CategoryId != null && product.CategoryId != 0)
                        productModel.CategoryName = _categoryService.GetByIdAsync((int)product.CategoryId).Result.Name;
                    return productModel;
                });
            });

            return model;
        }

        public virtual async Task<List<ProductTicketCategoryMapModel>> PrepareSingalProductTicketCategoryMapListModelAsync(int productId, int venueId)
        {
            List<ProductTicketCategoryMapModel> productTicketCategoryMapModels = new List<ProductTicketCategoryMapModel>();
            var productTicketCategoryMaps = await _productTicketCategoryMapService.GetProductTicketCategoryMapsByProductIdAsync(productId);
            var venueTicketCategoryMaps = await _venueTicketCategoryMapService.GetVenueTicketCategoryMapsByVenueIdAsync(venueId);
            var ticketCategories = (await _ticketCategoryService.GetTicketCategoryAsync(string.Empty)).ToList();
            List<VenueTicketCategoryMapModel> venueTicketCategoryMapsModel = new List<VenueTicketCategoryMapModel>();
            foreach (var venueTicketCategoryMap in venueTicketCategoryMaps)
            {
                ProductTicketCategoryMapModel model = new ProductTicketCategoryMapModel();
                model.Id = venueTicketCategoryMap.Id;

                var ticketCategory = ticketCategories.FirstOrDefault(x => x.Id == venueTicketCategoryMap.TicketCategoryId);
                if (ticketCategory != null)
                {
                    model.TicketCategoryId = ticketCategory.Id;
                    model.TicketCategoryName = ticketCategory.Name;
                    model.File = await _mediaFactoryModel.GetRequestModelAsync(ticketCategory.FileId);
                }

                var productTicketCategoryMap = productTicketCategoryMaps.FirstOrDefault(x => x.TicketCategoryId == venueTicketCategoryMap.TicketCategoryId);
                if (productTicketCategoryMap != null)
                {
                    model.Total = productTicketCategoryMap.Total;
                    model.Available = productTicketCategoryMap.Available;
                    model.Block = productTicketCategoryMap.Block;
                    model.Sold = productTicketCategoryMap.Sold;
                    model.Price = productTicketCategoryMap.Price;
                }

                productTicketCategoryMapModels.Add(model);
            }
            return productTicketCategoryMapModels;
        }

        public virtual async Task<List<ProductTicketCategoryMapModel>> PrepareComboProductTicketCategoryMapListModelAsync(int? productId, List<int> productMapIds)
        {
            List<ProductTicketCategoryMap> productTicketCategoryMaps = new List<ProductTicketCategoryMap>();
            if (productId.HasValue)
            {
                productTicketCategoryMaps = (await _productTicketCategoryMapService.GetProductTicketCategoryMapsByProductIdAsync(productId.Value)).ToList();
            }
            var productsTicketCategoryMaps = await _productTicketCategoryMapService.GetProductTicketCategoryMapsByMultipleProductIdsAsync(productMapIds);
            var productsTicketCategoryGroupsMaps = productsTicketCategoryMaps.GroupBy(x => x.TicketCategoryId);
            var products = (await _productService.GetByProductMapIdsAsync(productMapIds)).ToList();
            var venueIds = products.Where(x => x.VenueId.HasValue).Select(x => x.VenueId.Value).ToList();
            var venueTicketCategoryMaps = await _venueTicketCategoryMapService.GetVenueTicketCategoryMapsByVenueIdsAsync(venueIds);
            var venueTicketCategoryGroupMaps = venueTicketCategoryMaps.GroupBy(x => x.TicketCategoryId);
            var ticketCategories = (await _ticketCategoryService.GetTicketCategoryAsync(string.Empty)).ToList();

            List<ProductTicketCategoryMapModel> productTicketCategoryMapModels = new List<ProductTicketCategoryMapModel>();

            foreach (var venueTicketCategoryMap in venueTicketCategoryGroupMaps)
            {
                var ticketCategoryId = venueTicketCategoryMap.Key;
                ProductTicketCategoryMapModel model = new ProductTicketCategoryMapModel();
                model.TicketCategoryId = ticketCategoryId;

                var ticketCategory = ticketCategories.FirstOrDefault(x => x.Id == ticketCategoryId);
                if (ticketCategory != null)
                {
                    model.TicketCategoryId = ticketCategory.Id;
                    model.TicketCategoryName = ticketCategory.Name;
                    model.File = await _mediaFactoryModel.GetRequestModelAsync(ticketCategory.FileId);
                }

                var productTicketCategoryMap = productTicketCategoryMaps.FirstOrDefault(x => x.TicketCategoryId == ticketCategoryId);
                if (productTicketCategoryMap != null)
                {
                    model.Id = productTicketCategoryMap.Id;
                    model.Total = productTicketCategoryMap.Total;
                    model.Available = productTicketCategoryMap.Available;
                    model.Block = productTicketCategoryMap.Block;
                    model.Sold = productTicketCategoryMap.Sold;
                    model.Price = productTicketCategoryMap.Price;
                }
                else
                {
                    var productsTicketCategoryGroupMaps = productsTicketCategoryGroupsMaps.FirstOrDefault(x => x.Key == ticketCategoryId);
                    if (productsTicketCategoryGroupMaps != null)
                    {
                        model.Total = productsTicketCategoryGroupMaps.Min(x=>x.Total);
                        model.Price = productsTicketCategoryGroupMaps.Average(x=>x.Price);
                    }
                }
                productTicketCategoryMapModels.Add(model);
            }

            return productTicketCategoryMapModels;
        }


        //public virtual async Task<List<ProductTicketCategoryMapModel>> PrepareProductTicketCategoryMapListByProductIdsModelAsync(List<int> productIds)
        //{
        //    List<ProductTicketCategoryMapModel> productTicketCategoryMapModels = new List<ProductTicketCategoryMapModel>();
        //    var productTicketCategoryMaps = await _productTicketCategoryMapService.GetProductTicketCategoryMapsByMultipleProductIdsAsync(productIds);
        //    //var venueTicketCategoryMaps = await _venueTicketCategoryMapService.GetVenueTicketCategoryMapsByVenueIdAsync(venueId);
        //    var ticketCategories = (await _ticketCategoryService.GetTicketCategoryAsync(string.Empty)).ToList();
        //    foreach (var venueTicketCategoryMap in productTicketCategoryMaps)
        //    {
        //        ProductTicketCategoryMapModel model = new ProductTicketCategoryMapModel();
        //        model.Id = venueTicketCategoryMap.Id;

        //        var ticketCategory = ticketCategories.FirstOrDefault(x => x.Id == venueTicketCategoryMap.TicketCategoryId);
        //        if (ticketCategory != null)
        //        {
        //            model.TicketCategoryId = ticketCategory.Id;
        //            model.TicketCategoryName = ticketCategory.Name;
        //            model.File = await _mediaFactoryModel.GetRequestModelAsync(ticketCategory.FileId);
        //        }

        //        var productTicketCategoryMap = productTicketCategoryMaps.FirstOrDefault(x => x.TicketCategoryId == venueTicketCategoryMap.TicketCategoryId);
        //        if (productTicketCategoryMap != null)
        //        {
        //            model.Total = productTicketCategoryMap.Total;
        //            model.Available = productTicketCategoryMap.Available;
        //            model.Block = productTicketCategoryMap.Block;
        //            model.Sold = productTicketCategoryMap.Sold;
        //            model.Price = productTicketCategoryMap.Price;
        //        }

        //        productTicketCategoryMapModels.Add(model);
        //    }
        //    return productTicketCategoryMapModels;
        //}
    }
}
