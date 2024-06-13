using G20.Core;
using G20.Core.Domain;
using G20.Data;
using System.Net.Sockets;

namespace G20.Service.Tickets
{
    public class TicketService : ITicketService
    {
        protected readonly IRepository<Ticket> _entityRepository;

        public TicketService(IRepository<Ticket> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<Ticket>> GetTicketsAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var tickets = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(t => t.Available.ToString().Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return tickets;
        }

        public virtual async Task<Ticket> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(Ticket entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(Ticket entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Ticket entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
