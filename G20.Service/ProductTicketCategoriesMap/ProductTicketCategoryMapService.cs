using G20.Core.Domain;
using G20.Core;
using G20.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.ProductTicketCategoriesMap
{
    public class ProductTicketCategoryMapService : IProductTicketCategoryMapService
    {
        protected readonly IRepository<ProductTicketCategoryMap> _entityRepository;

        public ProductTicketCategoryMapService(IRepository<ProductTicketCategoryMap> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<ProductTicketCategoryMap>> GetProductTicketCategoryMapsAsync(int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var maps = await _entityRepository.GetAllPagedAsync(query =>
            {
                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return maps;
        }

        public virtual async Task<IList<ProductTicketCategoryMap>> GetProductTicketCategoryMapsByProductIdAsync(int productId)
        {
            var maps = await _entityRepository.Table.Where(x => x.IsDeleted == false && x.ProductId == productId).ToListAsync();
            return maps;
        }

        public virtual async Task<ProductTicketCategoryMap> GetByIdAsync(int Id)
        {
            return await _entityRepository.GetByIdAsync(Id);
        }

        public virtual async Task InsertAsync(ProductTicketCategoryMap entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(ProductTicketCategoryMap entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(ProductTicketCategoryMap entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }

        public virtual async Task<IList<ProductTicketCategoryMap>> GetProductTicketCategoryMapsByMultipleProductIdsAsync(List<int> productIds)
        {
            var maps = await _entityRepository.Table.Where(x => x.IsDeleted == false && productIds.Contains(x.ProductId)).ToListAsync();
            return maps;
        }
    }
}
