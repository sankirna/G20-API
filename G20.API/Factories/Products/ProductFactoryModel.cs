using G20.API.Factories.Media;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.ProductCombos;
using G20.API.Models.Products;
using G20.API.Models.ProductTicketCategoriesMap;
using G20.API.Models.Teams;
using G20.API.Models.VenueTicketCategoriesMap;
using G20.Core.Domain;
using G20.Service.Categories;
using G20.Service.Countries;
using G20.Service.ProductCombos;
using G20.Service.Products;
using G20.Service.ProductTicketCategoriesMap;
using G20.Service.Teams;
using G20.Service.TicketCategories;
using G20.Service.Venue;
using G20.Service.VenueTicketCategoriesMap;
using Microsoft.CodeAnalysis;
using LinqToDB;
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
        protected readonly IProductComboService _productComboService;

        public ProductFactoryModel(
              IProductService productService
            , IVenueService venueService
            , ICountryService countryService
            , IVenueTicketCategoryMapService venueTicketCategoryMapService
            , IMediaFactoryModel mediaFactoryModel
            , ITicketCategoryService ticketCategoryService
            , IProductTicketCategoryMapService productTicketCategoryMapService
            , ITeamService teamService
            , ICategoryService categoryService
            , IProductComboService productComboService
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
            _productComboService = productComboService;
        }

        public virtual async Task<ProductModel> PrepareProductModelAsync(int productId)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product == null)
                return null;
            var model = product.ToModel<ProductRequestModel>();
            model.File = await _mediaFactoryModel.GetRequestModelAsync(model.FileId);
            return model;
        }

        public virtual async Task<ProductListModel> PrepareProductListModelAsync(ProductSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var products = await _productService.GetProductsAsync(name: searchModel.Name, productTypeId: searchModel.ProductTypeId,
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

        public virtual async Task<ProductTicketCategoryMapModel> PrepareProductTicketCategoryMapModelAsync(int productTicketCategoryMapId)
        {
            var productTicketCategoryMap = await _productTicketCategoryMapService.GetByIdAsync(productTicketCategoryMapId);
            var ticketCategory = await _ticketCategoryService.GetByIdAsync(productTicketCategoryMap.Id);
            ProductTicketCategoryMapModel model = new ProductTicketCategoryMapModel();
            if (ticketCategory != null)
            {
                model.TicketCategoryId = ticketCategory.Id;
                model.TicketCategoryName = ticketCategory.Name;
                model.File = await _mediaFactoryModel.GetRequestModelAsync(ticketCategory.FileId);
                if (productTicketCategoryMap != null)
                {
                    model.Id = productTicketCategoryMap.Id;
                    model.ProductId = productTicketCategoryMap.ProductId;
                    model.Total = productTicketCategoryMap.Total;
                    model.Available = productTicketCategoryMap.Available;
                    model.Block = productTicketCategoryMap.Block;
                    model.Sold = productTicketCategoryMap.Sold;
                    model.Price = productTicketCategoryMap.Price;
                }
            }
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
                //model.p = venueTicketCategoryMap.Id;

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
                    model.Id = productTicketCategoryMap.Id;
                    model.ProductId = productTicketCategoryMap.ProductId;
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
            var products = (await _productService.GetByProductIdsAsync(productMapIds)).ToList();
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
                        model.Total = productsTicketCategoryGroupMaps.Min(x => x.Total);
                        model.Price = productsTicketCategoryGroupMaps.Average(x => x.Price);
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

        public virtual async Task<ProductListForSiteModel> PrepareProductListForSiteModelAsync(ProductForSiteSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var products = await _productService.GetProductsForSiteAsync(name: searchModel.searchText, productTypeId: searchModel.ProductTypeId,
                teamId: searchModel.TeamId, categoryId: searchModel.CategoryId,
                minimumPrice: searchModel.MinimumPrice, maximumPrice: searchModel.MaximumPrice,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new ProductListForSiteModel().PrepareToGridAsync(searchModel, products, () =>
            {
                return products.SelectAwait(async product =>
                {

                    if (product.Team1Id != null)
                        product.Team1 = _teamService.GetByIdAsync((int)product.Team1Id).Result;
                    if (product.Team2Id != null)
                        product.Team2 = _teamService.GetByIdAsync((int)product.Team2Id).Result;

                    product.ProductTicketCategoryMaps = _productTicketCategoryMapService.GetProductTicketCategoryMapsByProductIdAsync(product.Id).Result.ToList().Where(p => p.Price >= searchModel.MinimumPrice && p.Price <= searchModel.MaximumPrice).ToList();
                    
                    var productModel = product.ToModel<ProductForSiteModel>();
                    if (product.VenueId != null)
                        productModel.VenueName = _venueService.GetByIdAsync((int)product.VenueId).Result.StadiumName;
                    if (product.CategoryId != null && product.CategoryId != 0)
                        productModel.CategoryName = _categoryService.GetByIdAsync((int)product.CategoryId).Result.Name;
                    productModel.File = await _mediaFactoryModel.GetRequestModelAsync(product.FileId);
                    productModel.ProductCombos = (await _productComboService.GetProductCombosByProductIdAsync(product.Id))
                                     .ToList().Select(c => c.ToModel<ProductComboModel>()).ToList();
                    if (product.ProductTicketCategoryMaps.Count > 0)
                    {
                        productModel.ProductTicketCategories =
                                            product.ProductTicketCategoryMaps.ToList().Select(c => c.ToModel<ProductTicketCategoryMapModel>()).ToList();
                        foreach (var item in productModel.ProductTicketCategories)
                        {
                            item.TicketCategoryName = _ticketCategoryService.GetByIdAsync(item.TicketCategoryId).Result.Name;
                        }
                        productModel.Price = "$" + product.ProductTicketCategoryMaps.Min(c => c.Price).ToString() + " - $" + product.ProductTicketCategoryMaps.Max(c => c.Price).ToString(); ;
                    }
                    
                    
                    var productIds = productModel.ProductCombos.Select(x => x.ProductMapId).ToList();
                    productModel.ProductTeamsList = new List<ProductModel>();
                    foreach (var item in productIds)
                    {
                        var productDetail = _productService.GetByIdAsync(item).Result;
                        
                        ProductModel teamProductModel = new ProductModel();
                        teamProductModel.Name = productDetail.Name;
                        teamProductModel.Team1Id = productDetail.Team1Id;
                        teamProductModel.Team2Id = productDetail.Team2Id;
                        teamProductModel.StartDateTime = productDetail.StartDateTime;
                        teamProductModel.EndDateTime = productDetail.EndDateTime;
                        teamProductModel.Description = productDetail.Description;                        
                        

                        //var teamProductModel =  productDetail.ToModel<ProductModel>;
                        if (productDetail.Team1Id != null)
                        {
                            productDetail.Team1 = _teamService.GetByIdAsync((int)productDetail.Team1Id).Result;
                            teamProductModel.Team1Name = productDetail.Team1.Name;
                            teamProductModel.Team1LogoUrl = productDetail.Team1.File?.Name;
                        }
                        if (productDetail.Team2Id != null)
                        {
                            productDetail.Team2 = _teamService.GetByIdAsync((int)productDetail.Team2Id).Result;
                            teamProductModel.Team2Name = productDetail.Team2.Name;
                            teamProductModel.Team2LogoUrl = productDetail.Team2.File?.Name;
                        }
                        if (productDetail.VenueId != null)
                        {
                            productDetail.Venue = _venueService.GetByIdAsync((int)productDetail.VenueId).Result;
                            teamProductModel.VenueName = productDetail.Venue.StadiumName;
                        }
                        teamProductModel.File = await _mediaFactoryModel.GetRequestModelAsync(productDetail.FileId);
                        if (teamProductModel != null)
                            productModel.ProductTeamsList.Add(teamProductModel);
                        
                        productDetail.ProductTicketCategoryMaps = _productTicketCategoryMapService.GetProductTicketCategoryMapsByProductIdAsync(productDetail.Id).Result.ToList().Where(p => p.Price >= searchModel.MinimumPrice && p.Price <= searchModel.MaximumPrice).ToList();
                        if (productDetail.ProductTicketCategoryMaps.Count > 0)
                        {
                            teamProductModel.ProductTicketCategories =
                                                   productDetail.ProductTicketCategoryMaps.ToList().Select(c => c.ToModel<ProductTicketCategoryMapModel>()).ToList();
                            teamProductModel.Price = "$" + product.ProductTicketCategoryMaps.Min(c => c.Price).ToString() + " - $" + product.ProductTicketCategoryMaps.Max(c => c.Price).ToString();
                            foreach (var items in teamProductModel.ProductTicketCategories)
                            {
                                items.TicketCategoryName = _ticketCategoryService.GetByIdAsync(items.TicketCategoryId).Result.Name;
                            }
                        } 
                    }

                    // var productTicketCategories = _productTicketCategoryMapService.GetProductTicketCategoryMapsByProductIdAsync(product.Id);
                    //productModel.ProductTicketCategories = productTicketCategories.Result.ToList();
                    //if (searchModel.MinimumPrice.HasValue || searchModel.MaximumPrice.HasValue)
                    //    productModel.ProductTicketCategories.Where(c => c.Price >= searchModel.MinimumPrice && c.Price >= searchModel.MaximumPrice);


                    return productModel;
                });
            });

            return model;
        }
    }
}
