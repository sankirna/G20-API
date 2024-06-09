using Matrimony.Core;
using Matrimony.Core.Domain;
using Matrimony.Data;

namespace Matrimony.Service.Occupations
{
    public class OccupationService : IOccupationService
    {
        protected readonly IRepository<Occupation> _occupationRepository;

        public OccupationService(IRepository<Occupation> occupationRepository)
        {
            _occupationRepository = occupationRepository;
        }

        public virtual async Task<IPagedList<Occupation>> GetOccupationsAsync(string name,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var countries = await _occupationRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.Name.Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return countries;
        }

        public virtual async Task<IList<Occupation>> GetByProfileIdAsync(int profileId)
        {
            return await _occupationRepository.GetAllAsync(query => query.Where(x => x.ProfileId == profileId), false);
        }

        public virtual async Task<Occupation> GetByIdAsync(int id)
        {
            return await _occupationRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Insert a occupation
        /// </summary>
        /// <param name="Occupation">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertAsync(Occupation entity)
        {
            await _occupationRepository.InsertAsync(entity);
        }

        /// <summary>
        /// Updates the occupation
        /// </summary>
        /// <param name="occupation">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateAsync(Occupation entity)
        {
            await _occupationRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Delete a occupation
        /// </summary>
        /// <param name="occupation">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteAsync(Occupation entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _occupationRepository.DeleteAsync(entity);
        }
    }
}
