using G20.Core;
using G20.Core.Domain;
using G20.Data;

namespace G20.Service.Venue
{
    public class VenueService : IVenueService
    {
        protected readonly IRepository<Core.Domain.Venue> _entityRepository;

        public VenueService(IRepository<Core.Domain.Venue> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<Core.Domain.Venue>> GetVenueAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var categories = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.StadiumName.Contains(name) || c.Location.Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return categories;
        }

        public virtual async Task<Core.Domain.Venue> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(Core.Domain.Venue entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(Core.Domain.Venue entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Core.Domain.Venue entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
