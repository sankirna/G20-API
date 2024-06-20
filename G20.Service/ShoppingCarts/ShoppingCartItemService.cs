using G20.Core.Domain;
using G20.Core;
using G20.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.ShoppingCarts
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        protected readonly IRepository<ShoppingCartItem> _entityRepository;

        public ShoppingCartItemService(IRepository<ShoppingCartItem> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<ShoppingCartItem>> GetShoppingCartItemsAsync(int shoppingCartId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var shoppingCartItems = await _entityRepository.GetAllPagedAsync(query =>
            {
                query = query.Where(c => c.ShoppingCartId == shoppingCartId);
                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return shoppingCartItems;
        }

        public virtual async Task<ShoppingCartItem> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(ShoppingCartItem entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(ShoppingCartItem entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(ShoppingCartItem entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
