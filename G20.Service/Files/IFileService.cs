using G20.Core;
using G20.Core.Domain;
using System.Threading.Tasks;
using File = G20.Core.Domain.File;

namespace G20.Service.Files
{
    public interface IFileService
    {
        Task<IPagedList<File>> GetFilesAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<File> GetByIdAsync(int id);
        Task InsertAsync(File entity);
        Task UpdateAsync(File entity);
        Task DeleteAsync(File entity);
    }
}
