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
using G20.Service.Venues;
using G20.Service.VenueTicketCategoriesMap;
using Microsoft.CodeAnalysis;
using LinqToDB;
using Nop.Web.Framework.Models.Extensions;
using G20.API.Models.Categories;
using G20.API.Factories.Categories;
using G20.API.Factories.Venues;
using G20.API.Factories.Teams;

namespace G20.API.Factories.Products
{
    public class ProductFactoryModel : IProductFactoryModel
    {
        protected readonly IProductService _productService;
        protected readonly IVenueService _venueService;
        protected readonly IVenueFactoryModel _venueFactoryModel;
        protected readonly ICountryService _countryService;
        protected readonly IVenueTicketCategoryMapService _venueTicketCategoryMapService;
        protected readonly IMediaFactoryModel _mediaFactoryModel;
        protected readonly ITicketCategoryService _ticketCategoryService;
        protected readonly ITeamService _teamService;
        protected readonly ITeamFactoryModel _teamFactoryModel;
        protected readonly IProductTicketCategoryMapService _productTicketCategoryMapService;
        protected readonly ICategoryService _categoryService;
        protected readonly ICategoryFactoryModel _categoryFactoryModel;
        protected readonly IProductComboService _productComboService;

        public ProductFactoryModel(
              IProductService productService
            , IVenueService venueService
            , IVenueFactoryModel venueFactoryModel
            , ICountryService countryService
            , IVenueTicketCategoryMapService venueTicketCategoryMapService
            , IMediaFactoryModel mediaFactoryModel
            , ITicketCategoryService ticketCategoryService
            , IProductTicketCategoryMapService productTicketCategoryMapService
            , ITeamService teamService
            , ITeamFactoryModel teamFactoryModel
            , ICategoryService categoryService
            , ICategoryFactoryModel categoryFactoryModel
            , IProductComboService productComboService

            )
        {
            _productService = productService;
            _venueService = venueService;
            _venueFactoryModel = venueFactoryModel;
            _countryService = countryService;
            _venueTicketCategoryMapService = venueTicketCategoryMapService;
            _mediaFactoryModel = mediaFactoryModel;
            _ticketCategoryService = ticketCategoryService;
            _productTicketCategoryMapService = productTicketCategoryMapService;
            _teamService = teamService;
            _teamFactoryModel = teamFactoryModel;
            _categoryService = categoryService;
            _productComboService = productComboService;
            _categoryFactoryModel = categoryFactoryModel;
        }

        public virtual async Task<ProductModel> PrepareProductDetailModelAsync(Product entity
        , ProductForSiteSearchModel productForSiteSearchModel = null
        , bool isCategoryDetail = false
        , bool isVenueDetail = false
        , bool isTeam1Detail = false
        , bool isTeam2Detail = false
        , bool isProductCombos = false
        , bool isProductTicketCategoryMap = false
        , bool isProductComboListDetail = false)
        {
            var model = entity.ToModel<ProductModel>();
            model.File = await _mediaFactoryModel.GetRequestModelAsync(model.FileId);
            if (isCategoryDetail && model.CategoryId != null && model.CategoryId != 0)
            {
                var category = await _categoryService.GetByIdAsync(model.CategoryId);
                model.CategoryDetail = await _categoryFactoryModel.PrepareCategoryModelAsync(category);
            }
            if (isProductTicketCategoryMap)
            {
                var productTicketCategoryMaps = (await _productTicketCategoryMapService.GetProductTicketCategoryMapsByProductIdAsync(model.Id))
                         .ToList();
                if (productTicketCategoryMaps.Any())
                {
                    model.ProductTicketCategories = new List<ProductTicketCategoryMapModel>();
                    foreach (var productTicketCategoryMap in productTicketCategoryMaps)
                    {
                        model.ProductTicketCategories.Add(await PrepareProductTicketCategoryMapModelAsync(productTicketCategoryMap));
                    }
                }
            }
            if (model.ProductTypeEnum == Core.Enums.ProductTypeEnum.Regular)
            {
                if (isVenueDetail && model.VenueId.HasValue)
                {
                    var venue = await _venueService.GetByIdAsync(model.VenueId.Value);
                    model.VenueDetail = await _venueFactoryModel.PrepareVenueModelAsync(venue);
                }
                if (isTeam1Detail && model.Team1Id.HasValue)
                {
                    var team1 = await _teamService.GetByIdAsync(model.Team1Id.Value);
                    model.Team1Detail = await _teamFactoryModel.PrepareTeamModelAsync(team1);
                }
                if (isTeam2Detail && model.Team2Id.HasValue)
                {
                    var team2 = await _teamService.GetByIdAsync(model.Team2Id.Value);
                    model.Team2Detail = await _teamFactoryModel.PrepareTeamModelAsync(team2);
                }

            }
            if (model.ProductTypeEnum == Core.Enums.ProductTypeEnum.Combo)
            {
                if (isProductCombos)
                {
                    var productCombos = await _productComboService.GetProductCombosByProductIdAsync(model.Id);
                    model.ProductCombos = productCombos.ToList().Select(c => c.ToModel<ProductComboModel>()).ToList();
                }
                if (isProductComboListDetail)
                {
                    var comboProductMapIds = model.ProductCombos.Select(x => x.ProductMapId).ToList();
                    var productComboDetails = await _productService.GetByProductIdsAsync(comboProductMapIds);
                    model.ProductComboDetails = new List<ProductModel>();
                    model.StartDateTime = productComboDetails.Min(x => x.StartDateTime);
                    model.EndDateTime = productComboDetails.Max(x => x.EndDateTime);
                    foreach (var productComboDetail in productComboDetails)
                    {
                        var productComboModelDetail = await PrepareProductDetailModelAsync(productComboDetail,
                                                                                      isVenueDetail: true,
                                                                                      isTeam1Detail: true,
                                                                                      isTeam2Detail: true);

                        model.ProductComboDetails.Add(productComboModelDetail);
                    }
                }
            }

            return model;
        }

        public virtual async Task<ProductModel> PrepareProductModelAsync(int productId)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product == null)
                return null;
            var model = await PrepareProductDetailModelAsync(product, isCategoryDetail: true, isVenueDetail: true, isTeam1Detail: true, isTeam2Detail: true);
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
                    var productModel = await PrepareProductDetailModelAsync(product,
                        isCategoryDetail: true,
                        isVenueDetail: true,
                        isTeam1Detail: true,
                        isTeam2Detail: true);
                    return productModel;
                });
            });

            return model;
        }

        public virtual async Task<ProductTicketCategoryMapModel> PrepareProductTicketCategoryMapModelAsync(int productTicketCategoryMapId)
        {
            var productTicketCategoryMap = await _productTicketCategoryMapService.GetByIdAsync(productTicketCategoryMapId);
            ProductTicketCategoryMapModel model = await PrepareProductTicketCategoryMapModelAsync(productTicketCategoryMap);
            return model;
        }

        public virtual async Task<ProductTicketCategoryMapModel> PrepareProductTicketCategoryMapModelAsync(ProductTicketCategoryMap enity)
        {
            ProductTicketCategoryMapModel model = new ProductTicketCategoryMapModel();
            if (enity != null)
            {
                model.Id = enity.Id;
                model.ProductId = enity.ProductId;
                model.Total = enity.Total;
                model.Available = enity.Available;
                model.Block = enity.Block;
                model.Sold = enity.Sold;
                model.Price = enity.Price;
                var ticketCategory = await _ticketCategoryService.GetByIdAsync(enity.Id);
                if (ticketCategory != null)
                {
                    model.TicketCategoryId = ticketCategory.Id;
                    model.TicketCategoryName = ticketCategory.Name;
                    model.File = await _mediaFactoryModel.GetRequestModelAsync(ticketCategory.FileId);
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
                    model.ProductId = productTicketCategoryMap.ProductId;
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

        public virtual async Task<ProductListForSiteModel> PrepareProductListForSiteModelAsync(ProductForSiteSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var products = await _productService.GetProductsForSiteAsync(name: searchModel.SearchText, productTypeId: searchModel.ProductTypeId,
                teamId: searchModel.TeamId, categoryId: searchModel.CategoryId,
                minimumPrice: searchModel.MinimumPrice, maximumPrice: searchModel.MaximumPrice,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new ProductListForSiteModel().PrepareToGridAsync(searchModel, products, () =>
            {
                return products.SelectAwait(async product =>
                {
                    var productModel = await PrepareProductDetailModelAsync(product,
                                                                             productForSiteSearchModel: searchModel,
                                                                             isCategoryDetail: true,
                                                                             isVenueDetail: true,
                                                                             isTeam1Detail: true,
                                                                             isTeam2Detail: true,
                                                                             isProductCombos: false,
                                                                             isProductTicketCategoryMap: true,
                                                                             isProductComboListDetail: false);
                    return productModel;
                });
            });

            return model;
        }
        public virtual async Task<ProductListModel> PrepareProductListByVenueModelAsync(ProductSearchByVenueModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var products = await _productService.GetProductsByVenueAsync(venueId: searchModel.VenueId,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new ProductListModel().PrepareToGridAsync(searchModel, products, () =>
            {
                return products.SelectAwait(async product =>
                {
                    var productModel = await PrepareProductDetailModelAsync(product,
                        isCategoryDetail: true,
                        isVenueDetail: true,
                        isTeam1Detail: true,
                        isTeam2Detail: true);
                    return productModel;
                });
            });

            return model;
        }
    }
}
