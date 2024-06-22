using G20.API.Models.Products;
using G20.API.Models.ProductTicketCategoriesMap;

namespace G20.API.Factories.Products
{
    public interface IProductFactoryModel
    {

        Task<ProductModel> PrepareProductModelAsync(int productId);
        Task<ProductListModel> PrepareProductListModelAsync(ProductSearchModel searchModel);
        Task<List<ProductTicketCategoryMapModel>> PrepareSingalProductTicketCategoryMapListModelAsync(int productId, int venueId);
        Task<ProductTicketCategoryMapModel> PrepareProductTicketCategoryMapModelAsync(int productTicketCategoryMapId);
        Task<List<ProductTicketCategoryMapModel>> PrepareComboProductTicketCategoryMapListModelAsync(int? productId, List<int> productMapIds);
    }
}
