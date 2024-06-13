using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Products;
using G20.Service.Products;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Factories.Products
{
    public class ProductFactoryModel : IProductFactoryModel
    {
        protected readonly IProductService _productService;

        public ProductFactoryModel(IProductService productService)
        {
            _productService = productService;
        }

        public virtual async Task<ProductListModel> PrepareProductListModelAsync(ProductSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var products = await _productService.GetProductsAsync(name: searchModel.Team,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new ProductListModel().PrepareToGridAsync(searchModel, products, () =>
            {
                return products.SelectAwait(async product =>
                {
                    var productModel = product.ToModel<ProductModel>();
                    return productModel;
                });
            });

            return model;
        }
    }
}
