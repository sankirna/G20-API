using Matrimony.Core.Domain;
using Matrimony.Core;

namespace Matrimony.Service.Addresss
{
    public interface IAddressService
    {
        Task<IPagedList<Address>> GetAddresssAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<IList<Address>> GetByProfileIdAsync(int profileId);
        Task<Address> GetByIdAsync(int Id);
        Task InsertAsync(Address entity);
        Task UpdateAsync(Address entity);
        Task DeleteAsync(Address entity);
    }
}
