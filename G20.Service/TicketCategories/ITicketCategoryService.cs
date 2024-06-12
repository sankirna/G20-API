using G20.Core.Domain;
using G20.Core;

namespace G20.Service.TicketCategories
{
    public interface ITicketCategoryService
    {
        Task<IPagedList<TicketCategory>> GetTicketCategoryAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<TicketCategory> GetByIdAsync(int id);
        Task InsertAsync(TicketCategory entity);
        Task UpdateAsync(TicketCategory entity);
        Task DeleteAsync(TicketCategory entity);
    }
}
