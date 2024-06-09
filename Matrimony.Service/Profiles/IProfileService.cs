using Matrimony.Core.Domain;
using Matrimony.Core;

namespace Matrimony.Service.Profiles
{
    public interface IProfileService
    {
        Task<IPagedList<Profile>> GetProfilesAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Profile> GetByIdAsync(int Id);
        Task<bool> CheckDuplicateAsync(int id, string email);
        Task InsertAsync(Profile entity);
        Task UpdateAsync(Profile entity);
        Task DeleteAsync(Profile entity);
    }
}
