using G20.Core.Domain;
using G20.Core;
using G20.Data;


namespace G20.Service.BoardingDetails
{
    public class BoardingDetailService : IBoardingDetailService
    {
        protected readonly IRepository<BoardingDetail> _entityRepository;

        public BoardingDetailService(IRepository<BoardingDetail> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<BoardingDetail>> GetBoardingDetailsAsync( int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var boardingDetails = await _entityRepository.GetAllPagedAsync(query =>
            {
                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return boardingDetails;
        }

        public virtual async Task<BoardingDetail> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(BoardingDetail entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(BoardingDetail entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(BoardingDetail entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
