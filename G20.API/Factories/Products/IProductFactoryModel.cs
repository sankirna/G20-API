using G20.API.Models.Products;

namespace G20.API.Factories.Products
{
    public interface IProductFactoryModel
    {
        Task<ProductListModel> PrepareProductListModelAsync(ProductSearchModel searchModel);
    }
}
