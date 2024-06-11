using G20.Core.Domain;
using G20.Core;

namespace G20.Service.StandCategories
{
    public interface IStandCategoryService
    {
        Task<IPagedList<Core.Domain.StandCategory>> GetStandCategoryAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Core.Domain.StandCategory> GetByIdAsync(int id);
        Task InsertAsync(Core.Domain.StandCategory entity);
        Task UpdateAsync(Core.Domain.StandCategory entity);
        Task DeleteAsync(Core.Domain.StandCategory entity);
    }
}
