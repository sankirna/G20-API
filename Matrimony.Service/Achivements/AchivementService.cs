using Matrimony.Core;
using Matrimony.Core.Domain;
using Matrimony.Data;

namespace Matrimony.Service.Achivements
{
    public class AchivementService : IAchivementService
    {
        protected readonly IRepository<Achivement> _achivementRepository;

        public AchivementService(IRepository<Achivement> achivementRepository)
        {
            _achivementRepository = achivementRepository;
        }

        public virtual async Task<IPagedList<Achivement>> GetAchivementsAsync(string name,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var countries = await _achivementRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.Name.Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return countries;
        }

        public virtual async Task<Achivement> GetByIdAsync(int id)
        {
            return await _achivementRepository.GetByIdAsync(id);
        }

        public virtual async Task<IList<Achivement>> GetByProfileIdAsync(int profileId)
        {
            return await _achivementRepository.GetAllAsync(query => query.Where(x => x.ProfileId == profileId));
        }

        /// <summary>
        /// Insert a achivement
        /// </summary>
        /// <param name="Achivement">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertAsync(Achivement entity)
        {
            await _achivementRepository.InsertAsync(entity);
        }

        /// <summary>
        /// Updates the achivement
        /// </summary>
        /// <param name="achivement">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateAsync(Achivement entity)
        {
            await _achivementRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Delete a achivement
        /// </summary>
        /// <param name="achivement">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteAsync(Achivement entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _achivementRepository.DeleteAsync(entity);
        }
    }
}
