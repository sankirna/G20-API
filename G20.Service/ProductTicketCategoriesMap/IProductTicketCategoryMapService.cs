using G20.Core;
using G20.Core.Domain;

namespace G20.Service.ProductTicketCategoriesMap
{
    public interface IProductTicketCategoryMapService
    {
        Task<IPagedList<ProductTicketCategoryMap>> GetProductTicketCategoryMapsAsync(int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<IList<ProductTicketCategoryMap>> GetProductTicketCategoryMapsByProductIdAsync(int productId);
        Task<ProductTicketCategoryMap> GetByIdAsync(int Id);
        Task InsertAsync(ProductTicketCategoryMap entity);
        Task UpdateAsync(ProductTicketCategoryMap entity);
        Task DeleteAsync(ProductTicketCategoryMap entity);
        Task<IList<ProductTicketCategoryMap>> GetProductTicketCategoryMapsByMultipleProductIdsAsync(string productId);
    }
}
