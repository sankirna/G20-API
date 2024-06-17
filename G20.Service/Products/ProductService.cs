using G20.Core;
using G20.Core.Domain;
using G20.Core.Enums;
using G20.Data;
using System.Linq;

namespace G20.Service.Products
{
    public class ProductService : IProductService
    {
        protected readonly IRepository<Product> _entityRepository;

        public ProductService(IRepository<Product> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<Product>> GetProductsAsync(string name, int? productTypeId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var products = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(p => p.Team1Id.ToString().Contains(name));

                if (productTypeId.HasValue)
                {
                    query = query.Where(p => p.ProductTypeId == productTypeId);
                }

                return query.OrderByDescending(c => c.Id);
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return products;
        }

        public virtual async Task<Product> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(Product entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(Product entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Product entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
