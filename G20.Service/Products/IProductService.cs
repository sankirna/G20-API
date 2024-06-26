using G20.Core;
using G20.Core.Domain;
using System.Threading.Tasks;

namespace G20.Service.Products
{
    public interface IProductService
    {
        Task<IPagedList<Product>> GetProductsAsync(string name, int? productTypeId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Product> GetByIdAsync(int id);
        Task<IList<Product>> GetByProductIdsAsync(List<int> productIds);
        Task InsertAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task DeleteAsync(Product entity);
        Task<IPagedList<Product>> GetProductsForSiteAsync(string name, int? productTypeId, int? teamId, int? categoryId, decimal? minimumPrice, decimal? maximumPrice, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<IPagedList<Product>> GetProductsByVenueAsync(int venueId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
    }
}
