using G20.Core;
using G20.Core.Domain;
using G20.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace G20.Service.Sponsors
{
    public class SponsorService : ISponsorService
    {
        protected readonly IRepository<Sponsor> _entityRepository;

        public SponsorService(IRepository<Sponsor> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<Sponsor>> GetSponsorsAsync(string name, int countryId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var Sponsors = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(s => s.Name.Contains(name));

             query.Include(x => x.Country);

                if (countryId > 0)
                    query = query.Where(c => c.CountryId == countryId);

                return query.Include(x=>x.Cities);
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return Sponsors;
        }

        public virtual async Task<Sponsor> GetByIdAsync(int Id)
        {
            return await _entityRepository.GetByIdAsync(Id);
        }

        public virtual async Task InsertAsync(Sponsor entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(Sponsor entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Sponsor entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
