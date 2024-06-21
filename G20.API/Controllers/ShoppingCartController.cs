using G20.API.Factories.ShoppingCarts;
using G20.API.Models.ShoppingCarts;
using G20.Core;
using Microsoft.AspNetCore.Mvc;

namespace G20.API.Controllers
{
    public class ShoppingCartController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IShoppingCartFactory _shoppingCartFactory;

        public ShoppingCartController(IWorkContext workContext
            , IShoppingCartFactory shoppingCartFactory)
        {
            _workContext = workContext;
            _shoppingCartFactory = shoppingCartFactory;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get()
        {
            var shoppingCartModel = await _shoppingCartFactory.GetShoppingCartDetailByUserId(_workContext.GetCurrentUserId());

            return Success(shoppingCartModel);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(ShoppingCartModel model)
        {
            return Success("OK");
        }
    }
}
