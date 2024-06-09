using Matrimony.Core;
using Matrimony.Core.Domain;
using Matrimony.Data;

using File = Matrimony.Core.Domain.File;

namespace Matrimony.Service.Files
{
    public class FileService : IFileService
    {
        protected readonly IRepository<File> _entityRepository;

        public FileService(IRepository<File> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<File>> GetFilesAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var files = await _entityRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(f => f.Name.Contains(name));

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return files;
        }

        public virtual async Task<File> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Insert a file
        /// </summary>
        /// <param name="entity">File</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertAsync(File entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        /// <summary>
        /// Updates the file
        /// </summary>
        /// <param name="entity">File</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateAsync(File entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Delete a file
        /// </summary>
        /// <param name="entity">File</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteAsync(File entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _entityRepository.DeleteAsync(entity);
        }
    }
}
