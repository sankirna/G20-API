using G20.Core.Domain;
using G20.Core;
using G20.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.VenueTicketCategoriesMap
{
    public class VenueTicketCategoryMapService : IVenueTicketCategoryMapService
    {
        protected readonly IRepository<VenueTicketCategoryMap> _entityRepository;
        public VenueTicketCategoryMapService(IRepository<VenueTicketCategoryMap> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<VenueTicketCategoryMap>> GetVenueTicketCategoryMapsAsync(int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var maps = await _entityRepository.GetAllPagedAsync(query =>
            {
                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return maps;
        }

        public virtual async Task<IList<VenueTicketCategoryMap>> GetVenueTicketCategoryMapsByVenueIdAsync(int venueId)
        {
            var maps = await GetVenueTicketCategoryMapsByVenueIdsAsync(new List<int>() { venueId });
            return maps;
        }

        public virtual async Task<IList<VenueTicketCategoryMap>> GetVenueTicketCategoryMapsByVenueIdsAsync(List<int> venueIds)
        {
            var maps = await _entityRepository.Table.Where(x => x.IsDeleted == false && venueIds.Contains(x.VenueId)).ToListAsync();
            return maps;
        }


        public virtual async Task<VenueTicketCategoryMap> GetByIdAsync(int Id)
        {
            return await _entityRepository.GetByIdAsync(Id);
        }

        public virtual async Task InsertAsync(VenueTicketCategoryMap entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(VenueTicketCategoryMap entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(VenueTicketCategoryMap entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
