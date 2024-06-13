using G20.Core;
using G20.Core.Domain;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace G20.Service.Tickets
{
    public interface ITicketService
    {
        Task<IPagedList<Ticket>> GetTicketsAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Ticket> GetByIdAsync(int id);
        Task InsertAsync(Ticket entity);
        Task UpdateAsync(Ticket entity);
        Task DeleteAsync(Ticket entity);
    }
}
