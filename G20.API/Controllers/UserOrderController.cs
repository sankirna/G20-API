using G20.API.Factories.Orders;
using G20.API.Models.Orders;
using G20.Core;
using Microsoft.AspNetCore.Mvc;

namespace G20.API.Controllers
{
    public class UserOrderController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IOrderFactory _orderFactory;

        public UserOrderController(IWorkContext workContext
            , IOrderFactory orderFactory)
        {
            _workContext = workContext;
            _orderFactory = orderFactory;
        }

        [HttpPost]
        public virtual async Task<IActionResult> GetOrderHistory(UserOrderHistoryRequestModel searchModel)
        {
            searchModel.UserId=_workContext.GetCurrentUserId();
            var model = await _orderFactory.PrepareOrderListModelAsync(searchModel);
            return Success(model);
        }
    }
}
