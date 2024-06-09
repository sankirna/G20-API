using G20.Core.Domain;
using G20.Core;
using System.Threading.Tasks;

namespace G20.Service.Categories
{
    public interface ICategoryService
    {
        Task<IPagedList<Category>> GetCategoriesAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Category> GetByIdAsync(int id);
        Task InsertAsync(Category entity);
        Task UpdateAsync(Category entity);
        Task DeleteAsync(Category entity);
    }
}
