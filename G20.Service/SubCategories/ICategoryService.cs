using G20.Core.Domain;
using G20.Core;
using System.Threading.Tasks;

namespace G20.Service.SubCategories
{
    public interface ISubCategoryService
    {
        Task<IPagedList<SubCategory>> GetSubCategoriesAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<SubCategory> GetByIdAsync(int id);
        Task InsertAsync(SubCategory entity);
        Task UpdateAsync(SubCategory entity);
        Task DeleteAsync(SubCategory entity);
    }
}
