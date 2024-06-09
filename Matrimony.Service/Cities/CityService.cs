using Matrimony.Core;
using Matrimony.Core.Domain;
using Matrimony.Data;
using Microsoft.EntityFrameworkCore;

namespace Matrimony.Service.Cities
{
    public class CityService : ICityService
    {
        protected readonly IRepository<City> _entityRepository;

        public CityService(IRepository<City> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<City>> GetCitiesAsync(string name, int stateId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var cities = await _entityRepository.GetAllPagedAsync(query =>
            {

                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.Name.Contains(name));

                if (stateId > 0)
                    query = query.Where(c => c.StateId == stateId);

                query = query.Include(x => x.State);
                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);


            return cities;
        }

        public virtual async Task<City> GetByIdAsync(int Id)
        {
            return await _entityRepository.GetByIdAsync(Id);
        }

        public virtual async Task InsertAsync(City entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(City entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(City entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
