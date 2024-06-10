using G20.Core.Domain;
using G20.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Venue
{
    public interface IVenueService
    {
        Task<IPagedList<Venues>> GetVenueAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Venues> GetByIdAsync(int id);
        Task InsertAsync(Venues entity);
        Task UpdateAsync(Venues entity);
        Task DeleteAsync(Venues entity);
    }
}
