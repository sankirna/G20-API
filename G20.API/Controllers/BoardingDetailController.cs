using G20.API.Controllers;
using G20.API.Models.BoardingDetails;
using G20.Core.Domain;
using G20.Core;
using G20.Service.BoardingDetails;
using Matrimony.API.Factories.BoardingDetails;
using Matrimony.API.Models.BoardingDetails;
using Microsoft.AspNetCore.Mvc;
using G20.API.Infrastructure.Mapper.Extensions;

namespace Matrimony.API.Controllers
{
    public class BoardingDetailController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IBoardingDetailFactoryModel _boardingDetailFactoryModel;
        protected readonly IBoardingDetailService _boardingDetailService;

        public BoardingDetailController(IWorkContext workContext,
            IBoardingDetailFactoryModel boardingDetailFactoryModel,
            IBoardingDetailService boardingDetailService)
        {
            _workContext = workContext;
            _boardingDetailFactoryModel = boardingDetailFactoryModel;
            _boardingDetailService = boardingDetailService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(BoardingDetailSearchModel searchModel)
        {
            var model = await _boardingDetailFactoryModel.PrepareBoardingDetailListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var boardingDetail = await _boardingDetailService.GetByIdAsync(id);
            if (boardingDetail == null)
                return Error("not found");
            return Success(boardingDetail.ToModel<BoardingDetailModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(BoardingDetailModel model)
        {
            var boardingDetail = model.ToEntity<BoardingDetail>();
            await _boardingDetailService.InsertAsync(boardingDetail);
            return Success(boardingDetail.ToModel<BoardingDetailModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(BoardingDetailModel model)
        {
            var boardingDetail = await _boardingDetailService.GetByIdAsync(model.Id);
            if (boardingDetail == null)
                return Error("not found");

            boardingDetail = model.ToEntity(boardingDetail);
            await _boardingDetailService.UpdateAsync(boardingDetail);
            return Success(boardingDetail.ToModel<BoardingDetailModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var boardingDetail = await _boardingDetailService.GetByIdAsync(id);
            if (boardingDetail == null)
                return Error("not found");
            await _boardingDetailService.DeleteAsync(boardingDetail);
            return Success(id);
        }

        [HttpPost]
        public virtual async Task<IActionResult> ValidateTicket(BoardingCheckRequestModel model)
        {
            return Success("ok");
        }
    }
}
