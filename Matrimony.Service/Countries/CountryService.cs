using Matrimony.Core;
using Matrimony.Core.Domain;
using Matrimony.Data;
using Microsoft.EntityFrameworkCore;

namespace Matrimony.Service.Countries
{
    public class CountryService : ICountryService
    {
        protected readonly IRepository<Country> _entityRepository;
        public CountryService(IRepository<Country> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<Country>> GetCountriesAsync(string name,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var countries = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.Name.Contains(name));
                query = query.Include(x => x.States);
                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return countries;
        }

        public virtual async Task<Country> GetByIdAsync(int Id)
        {
            return await _entityRepository.GetByIdAsync(Id);
        }

        /// <summary>
        /// Insert a entity
        /// </summary>
        /// <param name="Country">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertAsync(Country entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        /// <summary>
        /// Updates the entity
        /// </summary>
        /// <param name="entity">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateAsync(Country entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Delete a entity
        /// </summary>
        /// <param name="entity">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteAsync(Country entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _entityRepository.DeleteAsync(entity);
        }
    }
}
