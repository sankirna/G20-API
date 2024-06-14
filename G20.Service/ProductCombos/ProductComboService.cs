using G20.Core.Domain;
using G20.Core;
using G20.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G20.Service.ProductCombos
{
    public class ProductComboService : IProductComboService
    {
        protected readonly IRepository<ProductCombo> _entityRepository;

        public ProductComboService(IRepository<ProductCombo> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<ProductCombo>> GetProductCombosAsync(int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var combos = await _entityRepository.GetAllPagedAsync(query =>
            {
                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return combos;
        }

        public virtual async Task<IList<ProductCombo>> GetProductCombosByProductIdAsync(int productId)
        {
            var combos = await _entityRepository.Table.Where(x => !x.IsDeleted && x.ProductId == productId).ToListAsync();
            return combos;
        }

        public virtual async Task<ProductCombo> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(ProductCombo entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(ProductCombo entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(ProductCombo entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
