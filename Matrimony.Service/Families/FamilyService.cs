using Matrimony.Core;
using Matrimony.Core.Domain;
using Matrimony.Data;

namespace Matrimony.Service.Families
{
    public class FamilyService : IFamilyService
    {
        protected readonly IRepository<Family> _entityRepository;
        public FamilyService(IRepository<Family> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<Family>> GetFamiliesAsync(string name,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var families = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.Name.Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return families;
        }

        public virtual async Task<IList<Family>> GetByProfileIdAsync(int profileId)
        {
            return await _entityRepository.GetAllAsync(query => query.Where(x => x.ProfileId == profileId), false);
        }

        public virtual async Task<Family> GetByIdAsync(int Id)
        {
            return await _entityRepository.GetByIdAsync(Id);
        }

        /// <summary>
        /// Insert a entity
        /// </summary>
        /// <param name="Family">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertAsync(Family entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        /// <summary>
        /// Updates the entity
        /// </summary>
        /// <param name="entity">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateAsync(Family entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Delete a entity
        /// </summary>
        /// <param name="entity">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteAsync(Family entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _entityRepository.DeleteAsync(entity);
        }
    }
}
