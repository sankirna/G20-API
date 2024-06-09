using Matrimony.Core;
using Matrimony.Core.Domain;
using Matrimony.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pipelines.Sockets.Unofficial.SocketConnection;

namespace Matrimony.Service.Profiles
{
    public class ProfileService : IProfileService
    {
        protected readonly IRepository<Profile> _profileRepository;

        public ProfileService(IRepository<Profile> profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public virtual async Task<IPagedList<Profile>> GetProfilesAsync(string name,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var countries = await _profileRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.FirstName.Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return countries;
        }

        public virtual async Task<Profile> GetByIdAsync(int id)
        {
            return await _profileRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Insert a profile
        /// </summary>
        /// <param name="Profile">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<bool> CheckDuplicateAsync(int id, string email)
        {
            return await _profileRepository.Table.AnyAsync(x => x.Id != id && x.Email == email);
        }

        /// <summary>
        /// Insert a profile
        /// </summary>
        /// <param name="Profile">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertAsync(Profile entity)
        {
            await _profileRepository.InsertAsync(entity);
        }

        /// <summary>
        /// Updates the profile
        /// </summary>
        /// <param name="profile">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateAsync(Profile entity)
        {
            await _profileRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Delete a profile
        /// </summary>
        /// <param name="profile">Customer</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteAsync(Profile entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _profileRepository.DeleteAsync(entity);
        }
    }
}
