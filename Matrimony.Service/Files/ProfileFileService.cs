using Matrimony.Core;
using Matrimony.Core.Domain;
using Matrimony.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Matrimony.Service.ProfileFiles
{
    public class ProfileFileService : IProfileFileService
    {
        protected readonly IRepository<ProfileFile> _entityRepository;

        public ProfileFileService(IRepository<ProfileFile> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<ProfileFile>> GetFilesAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var files = await _entityRepository.GetAllPagedAsync(query =>
            {
                //if (!string.IsNullOrWhiteSpace(name))
                //    query = query.Where(f => f.Name.Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return files;
        }

        public virtual async Task<ProfileFile> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Insert a file
        /// </summary>
        /// <param name="entity">File</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertAsync(ProfileFile entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        /// <summary>
        /// Updates the file
        /// </summary>
        /// <param name="entity">File</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateAsync(ProfileFile entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Delete a file
        /// </summary>
        /// <param name="entity">File</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteAsync(ProfileFile entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _entityRepository.DeleteAsync(entity);
        }
    }
}
