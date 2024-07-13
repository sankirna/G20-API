using G20.API.Models.Products;
using G20.API.Models.ProductTicketCategoriesMap;
using G20.Core.Domain;

namespace G20.API.Factories.Products
{
    public interface IProductFactoryModel
    {
        Task<ProductModel> PrepareProductDetailModelAsync(Product entity
                                                        , ProductForSiteSearchModel productForSiteSearchModel = null
                                                        , bool isCategoryDetail = false
                                                        , bool isVenueDetail = false
                                                        , bool isTeam1Detail = false
                                                        , bool isTeam2Detail = false
                                                        , bool isProductCombos = false
                                                        , bool isProductTicketCategoryMap = false
                                                       
                                                        , bool isProductComboListDetail = false);
        Task<ProductModel> PrepareProductModelAsync(int productId);
        Task<ProductListModel> PrepareProductListModelAsync(ProductSearchModel searchModel);
        Task<List<ProductTicketCategoryMapModel>> PrepareSingalProductTicketCategoryMapListModelAsync(int productId, int venueId);
        Task<ProductTicketCategoryMapModel> PrepareProductTicketCategoryMapModelAsync(int productTicketCategoryMapId, int quantity = 1);
        Task<ProductTicketCategoryMapModel> PrepareProductTicketCategoryMapModelAsync(ProductTicketCategoryMap enity, int quantity = 1);
        Task<List<ProductTicketCategoryMapModel>> PrepareComboProductTicketCategoryMapListModelAsync(int? productId, List<int> productMapIds);
        Task<ProductListForSiteModel> PrepareProductListForSiteModelAsync(ProductForSiteSearchModel searchModel);
        Task<ProductListModel> PrepareProductListByVenueModelAsync(ProductSearchByVenueModel searchModel);
    }
}
