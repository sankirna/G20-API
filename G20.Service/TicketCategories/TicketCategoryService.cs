using G20.Core;
using G20.Core.Domain;
using G20.Data;

namespace G20.Service.TicketCategories
{
    public class TicketCategoryService : ITicketCategoryService
    {
        protected readonly IRepository<TicketCategory> _entityRepository;

        public TicketCategoryService(IRepository<TicketCategory> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<TicketCategory>> GetTicketCategoryAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var categories = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.Name.Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return categories;
        }

        public virtual async Task<TicketCategory> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(TicketCategory entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(TicketCategory entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(TicketCategory entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
