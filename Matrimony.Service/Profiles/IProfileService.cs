using G20.Core.Domain;
using G20.Core;

namespace G20.Service.Profiles
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
