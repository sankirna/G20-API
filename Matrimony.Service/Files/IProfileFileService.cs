using Matrimony.Core;
using Matrimony.Core.Domain;
using System.Threading.Tasks;

namespace Matrimony.Service.ProfileFiles
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
