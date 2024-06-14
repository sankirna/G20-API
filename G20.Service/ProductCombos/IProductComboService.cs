using G20.Core.Domain;
using G20.Core;

namespace G20.Service.ProductCombos
{
    public interface IProductComboService
    {
        Task<IPagedList<ProductCombo>> GetProductCombosAsync(int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<IList<ProductCombo>> GetProductCombosByProductIdAsync(int productId);
        Task<ProductCombo> GetByIdAsync(int id);
        Task InsertAsync(ProductCombo entity);
        Task UpdateAsync(ProductCombo entity);
        Task DeleteAsync(ProductCombo entity);
    }
}
