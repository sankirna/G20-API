using G20.Core;
using G20.Core.Domain;
using G20.Data;

namespace G20.Service.StandCategories
{
    public class StandCategoryService : IStandCategoryService
    {
        protected readonly IRepository<Core.Domain.StandCategory> _entityRepository;

        public StandCategoryService(IRepository<Core.Domain.StandCategory> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<Core.Domain.StandCategory>> GetStandCategoryAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var categories = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.StandName.Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return categories;
        }

        public virtual async Task<Core.Domain.StandCategory> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(Core.Domain.StandCategory entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(Core.Domain.StandCategory entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Core.Domain.StandCategory entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
