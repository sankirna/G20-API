using G20.API.Controllers;
using G20.API.Models.BoardingDetails;
using G20.Core.Domain;
using G20.Core;
using G20.Service.BoardingDetails;
using Matrimony.API.Factories.BoardingDetails;
using Matrimony.API.Models.BoardingDetails;
using Microsoft.AspNetCore.Mvc;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.Service.Orders;
using StackExchange.Redis;
using G20.API.Factories.Orders;
using MailKit.Search;

namespace Matrimony.API.Controllers
{
    public class BoardingDetailController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IBoardingDetailFactoryModel _boardingDetailFactoryModel;
        protected readonly IBoardingDetailService _boardingDetailService;
        protected readonly IOrderProductItemDetailService _orderProductItemDetailService;
        protected readonly IOrderProductItemService _orderProductItemService;
        protected readonly IOrderService _orderService;
        protected readonly IOrderFactory _orderFactory;

        public BoardingDetailController(IWorkContext workContext,
            IBoardingDetailFactoryModel boardingDetailFactoryModel,
            IBoardingDetailService boardingDetailService,
            IOrderProductItemDetailService orderProductItemDetailService,
            IOrderProductItemService orderProductItemService,
            IOrderService orderService,
            IOrderFactory orderFactory)
        {
            _workContext = workContext;
            _boardingDetailFactoryModel = boardingDetailFactoryModel;
            _boardingDetailService = boardingDetailService;
            _orderProductItemDetailService = orderProductItemDetailService;
            _orderProductItemService = orderProductItemService;
            _orderService = orderService;
            _orderFactory = orderFactory;
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
            if (model.Quantity < 0)
            {
                return Error("Invalid quantity.");
            }
            int boardQuantity = _boardingDetailService.GetBoardingQuanity(model.OrderProductItemDetailId, model.TotalQuantity);            
            if (boardQuantity > 0 && model.Quantity<= model.TotalQuantity)
            {
                var boardingDetail = model.ToEntity<BoardingDetail>();
                await _boardingDetailService.InsertAsync(boardingDetail);
                return Success(boardingDetail.ToModel<BoardingDetailModel>());
            }
            else
            {
                return Error("Exceed the ticket limit.");
            }
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
            var orderProductItemDetail = await _orderProductItemDetailService.GetDetailsByQRCodeAsync(model.ProductId, model.ValidateQRcode);
            if (orderProductItemDetail == null)
            {
                return Error("QR Scan failed");
            }
            else
            {
                var UserProductItemDetail = _orderFactory.GetOrderProductItemDetailModelAsync(orderProductItemDetail);
                return Success(UserProductItemDetail);
            }

        }
    }
}
