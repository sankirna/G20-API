using G20.Core;
using G20.Core.Domain;
using G20.Data;
using System.Threading.Tasks;

namespace G20.Service.SubCategories
{
    public class SubCategoryService : ISubCategoryService
    {
        protected readonly IRepository<SubCategory> _entityRepository;

        public SubCategoryService(IRepository<SubCategory> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<SubCategory>> GetSubCategoriesAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var subCategories = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.Name.Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return subCategories;
        }

        public virtual async Task<SubCategory> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(SubCategory entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(SubCategory entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(SubCategory entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
