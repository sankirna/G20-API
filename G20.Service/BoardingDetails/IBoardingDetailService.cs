using G20.Core.Domain;
using G20.Core;


namespace G20.Service.BoardingDetails
{
    public interface IBoardingDetailService
    {
        Task<IPagedList<BoardingDetail>> GetBoardingDetailsAsync( int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<BoardingDetail> GetByIdAsync(int id);
        Task InsertAsync(BoardingDetail entity);
        Task UpdateAsync(BoardingDetail entity);
        Task DeleteAsync(BoardingDetail entity);
        int GetBoardingQuanity(int OrderProductItemDetailId);
    }
}
