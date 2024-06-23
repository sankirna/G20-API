using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.BoardingDetails;
using G20.Service.BoardingDetails;
using Matrimony.API.Models.BoardingDetails;
using Nop.Web.Framework.Models.Extensions;

namespace Matrimony.API.Factories.BoardingDetails
{
    public class BoardingDetailFactoryModel : IBoardingDetailFactoryModel
    {
        protected readonly IBoardingDetailService _boardingDetailService;
        public BoardingDetailFactoryModel(IBoardingDetailService boardingDetailService)
        {
            _boardingDetailService = boardingDetailService;
        }

        public virtual async Task<BoardingDetailListModel> PrepareBoardingDetailListModelAsync(BoardingDetailSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var boardingDetails = await _boardingDetailService.GetBoardingDetailsAsync(
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new BoardingDetailListModel().PrepareToGridAsync(searchModel, boardingDetails, () =>
            {
                return boardingDetails.SelectAwait(async boardingDetail =>
                {
                    var boardingDetailModel = boardingDetail.ToModel<BoardingDetailModel>();
                    return boardingDetailModel;
                });
            });

            return model;
        }
    }
}
