using G20.Core;
using G20.Core.Domain;
using G20.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace G20.Service.States
{
    public class StateService : IStateService
    {
        protected readonly IRepository<State> _entityRepository;

        public StateService(IRepository<State> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<State>> GetStatesAsync(string name, int countryId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var states = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(s => s.Name.Contains(name));

             query.Include(x => x.Country);

                if (countryId > 0)
                    query = query.Where(c => c.CountryId == countryId);

                return query.Include(x=>x.Cities);
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return states;
        }

        public virtual async Task<State> GetByIdAsync(int Id)
        {
            return await _entityRepository.GetByIdAsync(Id);
        }

        public virtual async Task InsertAsync(State entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(State entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(State entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
