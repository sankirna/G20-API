using Matrimony.Core;
using Matrimony.Core.Domain;
using Matrimony.Data;

namespace Matrimony.Service.Addresss
{
    public class AddressService : IAddressService
    {
        protected readonly IRepository<Address> _addressRepository;

        public AddressService(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public virtual async Task<IPagedList<Address>> GetAddresssAsync(string name,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var countries = await _addressRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.Address1.Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return countries;
        }

        public virtual async Task<IList<Address>> GetByProfileIdAsync(int profileId)
        {
            return await _addressRepository.GetAllAsync(query => query.Where(x => x.ProfileId == profileId), false);
        }

        public virtual async Task<Address> GetByIdAsync(int id)
        {
            return await _addressRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Insert a address
        /// </summary>
        /// <param name="Address">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertAsync(Address entity)
        {
            await _addressRepository.InsertAsync(entity);
        }

        /// <summary>
        /// Updates the address
        /// </summary>
        /// <param name="address">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateAsync(Address entity)
        {
            await _addressRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Delete a address
        /// </summary>
        /// <param name="address">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteAsync(Address entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _addressRepository.DeleteAsync(entity);
        }
    }
}
