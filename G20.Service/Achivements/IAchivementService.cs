using G20.Core.Domain;
using G20.Core;

namespace G20.Service.Achivements
{
    public interface IAchivementService
    {
        Task<IPagedList<Achivement>> GetAchivementsAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<IList<Achivement>> GetByProfileIdAsync(int profileId);
        Task<Achivement> GetByIdAsync(int Id);
        Task InsertAsync(Achivement entity);
        Task UpdateAsync(Achivement entity);
        Task DeleteAsync(Achivement entity);
    }
}
