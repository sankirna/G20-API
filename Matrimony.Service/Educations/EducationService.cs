using Matrimony.Core;
using Matrimony.Core.Domain;
using Matrimony.Data;

namespace Matrimony.Service.Educations
{
    public class EducationService : IEducationService
    {
        protected readonly IRepository<Education> _educationRepository;

        public EducationService(IRepository<Education> educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public virtual async Task<IPagedList<Education>> GetEducationsAsync(string name,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var countries = await _educationRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.Name.Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return countries;
        }

        public virtual async Task<IList<Education>> GetByProfileIdAsync(int profileId)
        {
            return await _educationRepository.GetAllAsync(query => query.Where(x => x.ProfileId == profileId), false);
        }

        public virtual async Task<Education> GetByIdAsync(int id)
        {
            return await _educationRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Insert a education
        /// </summary>
        /// <param name="Education">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertAsync(Education entity)
        {
            await _educationRepository.InsertAsync(entity);
        }

        /// <summary>
        /// Updates the education
        /// </summary>
        /// <param name="education">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateAsync(Education entity)
        {
            await _educationRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Delete a education
        /// </summary>
        /// <param name="education">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteAsync(Education entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _educationRepository.DeleteAsync(entity);
        }
    }
}
