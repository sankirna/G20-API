using G20.Core;
using G20.Core.Domain;
using System.Threading.Tasks;

namespace G20.Service.ProfileFiles
{
    public interface IProfileFileService
    {
        Task<IPagedList<ProfileFile>> GetFilesAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<ProfileFile> GetByIdAsync(int id);
        Task InsertAsync(ProfileFile entity);
        Task UpdateAsync(ProfileFile entity);
        Task DeleteAsync(ProfileFile entity);
    }
}
