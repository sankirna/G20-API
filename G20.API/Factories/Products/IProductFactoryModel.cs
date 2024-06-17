using G20.API.Models.Products;
using G20.API.Models.ProductTicketCategoriesMap;

namespace G20.API.Factories.Products
{
    public interface IProductFactoryModel
    {
        Task<ProductListModel> PrepareProductListModelAsync(ProductSearchModel searchModel);
        Task<List<ProductTicketCategoryMapModel>> PrepareProductTicketCategoryMapListModelAsync(int productId, int venueId);
        Task<List<ProductTicketCategoryMapModel>> PrepareProductTicketCategoryMapListByProductIdsModelAsync(List<int> productIds);
    }
}
