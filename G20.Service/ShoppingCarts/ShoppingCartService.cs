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
    public class ShoppingCartService : IShoppingCartService
    {
        protected readonly IRepository<ShoppingCart> _entityRepository;

        public ShoppingCartService(IRepository<ShoppingCart> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<ShoppingCart>> GetShoppingCartsAsync(int userId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var shoppingCarts = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (userId > 0)
                    query = query.Where(c => c.UserId == userId);
                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return shoppingCarts;
        }

        public virtual async Task<ShoppingCart> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task<ShoppingCart> GetByUserIdAsync(int id)
        {
            return await _entityRepository.Table.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public virtual async Task InsertAsync(ShoppingCart entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(ShoppingCart entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(ShoppingCart entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
