using G20.API.Factories.Orders;
using G20.API.Factories.ShoppingCarts;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.ShoppingCarts;
using G20.Core;
using G20.Core.Domain;
using G20.Service.ShoppingCarts;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;

namespace G20.API.Controllers
{
    public class ShoppingCartController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IShoppingCartService _shoppingCartService;
        protected readonly IShoppingCartItemService _shoppingCartItemService;
        protected readonly IShoppingCartFactory _shoppingCartFactory;
        protected readonly IOrderFactory _orderFactory;

        public ShoppingCartController(IWorkContext workContext
            , IShoppingCartService shoppingCartService
            , IShoppingCartItemService shoppingCartItemService
            , IShoppingCartFactory shoppingCartFactory
            , IOrderFactory orderFactory)
        {
            _workContext = workContext;
            _shoppingCartService = shoppingCartService;
            _shoppingCartItemService= shoppingCartItemService;
            _shoppingCartFactory = shoppingCartFactory;
            _orderFactory = orderFactory;
        }

        #region Private Method

        private async Task AddUpdateShoppingCartItem(int shoppingCartId,int userId, List<ShoppingCartItemModel> shoppingCartItemModels)
        {
            if (shoppingCartItemModels != null)
            {
                var shoppingCartItems = await _shoppingCartItemService.GetByShoppingCartIdAsync(shoppingCartId);

                var existingIds = shoppingCartItems.Select(x => x.Id);
                var requestIds = shoppingCartItemModels.Select(x => x.Id);
                var updateIds = requestIds.Intersect(existingIds);
                var deleteIds = existingIds.Except(requestIds);
                var addedIds = requestIds.Except(existingIds);

                var deleteShoppingCartItems = shoppingCartItems.Where(x => deleteIds.Contains(x.Id));
                foreach (var shoppingCartItem in deleteShoppingCartItems)
                {
                    await _shoppingCartItemService.DeleteAsync(shoppingCartItem);
                }

                var updateShoppingCartItemModels = shoppingCartItemModels.Where(x => updateIds.Contains(x.Id));
                foreach (var updateShoppingCartItemModel in updateShoppingCartItemModels)
                {
                    var shoppingCartItem = shoppingCartItems.FirstOrDefault(x => x.Id == updateShoppingCartItemModel.Id);
                    if (shoppingCartItem == null)
                        throw new NopException("product Combo Map not found");
                    shoppingCartItem.UserId = userId;
                    shoppingCartItem.ShoppingCartId = shoppingCartId;
                    shoppingCartItem = updateShoppingCartItemModel.ToEntity(shoppingCartItem);
                    await _shoppingCartItemService.UpdateAsync(shoppingCartItem);
                }

                var addShoppingCartItemModels = shoppingCartItemModels.Where(x => addedIds.Contains(x.Id));
                foreach (var shoppingCartItemModel in addShoppingCartItemModels)
                {
                    var shoppingCartItem = shoppingCartItemModel.ToEntity<ShoppingCartItem>();
                    shoppingCartItem.UserId = userId;
                    shoppingCartItem.ShoppingCartId = shoppingCartId;
                    await _shoppingCartItemService.InsertAsync(shoppingCartItem);
                }
            }
        }

        #endregion

        [HttpPost]
        public virtual async Task<IActionResult> Get()
        {
            var shoppingCartModel = await _shoppingCartFactory.GetShoppingCartDetailByUserId(_workContext.GetCurrentUserId());

            return Success(shoppingCartModel);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(ShoppingCartModel model)
        {
            var userId = _workContext.GetCurrentUserId();
            
            var shoppingCartId = 0;

            #region Check validation and map order model from shopping cart

            var orderModel = _orderFactory.MapOrderModelFromShoppingModel(model);
            var couponCodeInfo = await _orderFactory.GetAndValidateCouponInfoByCode(orderModel);
            if (couponCodeInfo != null)
            {
                orderModel.CouponId = couponCodeInfo.CouponId;
                orderModel.Discount= couponCodeInfo.Discount;
            }
            else
            {
                orderModel.CouponId = null;
                orderModel.CouponCode = null;
            }
            var productDetails = await _orderFactory.GetAndValidateProductDetails(orderModel.Items);
            var checkAvaibility = await _orderFactory.CheckProductTicketAvaibility(productDetails, orderModel.Items);
            if (!checkAvaibility)
            {
                throw new NopException("Some of product(s) are out of stock. please remove from cart");
            }

            #endregion

            #region Map shopping cart from order model 

            model = _shoppingCartFactory.MapShoppingModelFromOrderModel(orderModel);

            #endregion

            #region Insert/Delete Shopping cart

            var shoppingCart = await _shoppingCartService.GetByUserIdAsync(userId);
            if (shoppingCart != null)
            {
                shoppingCartId = shoppingCart.Id;
                shoppingCart = model.ToEntity(shoppingCart);
                shoppingCart.Id = shoppingCartId;
                await _shoppingCartService.UpdateAsync(shoppingCart);
            }
            else
            {
                shoppingCart = model.ToEntity<ShoppingCart>();
                shoppingCart.UserId = userId;
                await _shoppingCartService.InsertAsync(shoppingCart);
                shoppingCartId = shoppingCart.Id;
            }

            #endregion

            #region Insert/Update/Delete Shopping cart item

            var shoppingCartItems = await _shoppingCartItemService.GetByShoppingCartIdAsync(shoppingCartId);

            await AddUpdateShoppingCartItem(shoppingCartId,userId, model.Items);

            #endregion

            return Success(model);
        }
    }
}
