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
                    query = query.Where(p => p.Name.Contains(name));

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

        public virtual async Task<IList<Product>> GetByProductIdsAsync(List<int> productIds)
        {
            return await _entityRepository.Table.Where(x => !x.IsDeleted
                                                         && productIds.Contains(x.Id)).ToListAsync();
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

        public virtual async Task<IPagedList<Product>> GetProductsForSiteAsync(string name, int? productTypeId, int? teamId, int? categoryId, decimal? minimumPrice, decimal? maximumPrice, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var products = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(p => p.Team1.Name.ToString().Contains(name) || p.Team2.Name.ToString().Contains(name));
                if (teamId.HasValue && teamId != 0)
                    query = query.Where(c => c.Team1Id == teamId || c.Team2Id == teamId);
                if (categoryId.HasValue && categoryId != 0)
                    query = query.Where(c => c.CategoryId == categoryId);
                //if (productTypeId.HasValue && productTypeId != 0)
                //    query = query.Where(p => p.ProductTypeId == productTypeId);
                if (minimumPrice > 0)
                {
                    query = query.Where(c => c.ProductTicketCategoryMaps.Any(p => p.Price >= minimumPrice));
                }
                if (maximumPrice > 0)
                {
                    query.Where(c => c.ProductTicketCategoryMaps.Any(p => p.Price <= maximumPrice));
                }
                return query.Where(c => c.ScheduleDateTime <= DateTime.Now).OrderByDescending(c => c.Id);

            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return products;
        }
        public virtual async Task<IPagedList<Product>> GetProductsByVenueAsync(int venueId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var products = await _entityRepository.GetAllPagedAsync(query =>
            {

                if (venueId != null)
                {
                    query = query.Where(p => p.VenueId == venueId && p.StartDateTime.Value.Date == DateTime.Now.Date);
                }

                return query.OrderByDescending(c => c.Id);
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return products;
        }
    }
}
