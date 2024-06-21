using FluentValidation;
using G20.API.Models.Coupons;
using G20.API.Models.ShoppingCarts;
using System.Security.Policy;

namespace G20.API.Validators.ShoppingCarts
{
    public class ShoppingCartModelValidator : AbstractValidator<ShoppingCartModel>
    {
        public ShoppingCartModelValidator()
        {
            RuleFor(x => x.CouponCode).NotEmpty().WithMessage("CouponCode ");
            RuleFor(x => x.Items).NotEmpty().WithMessage("Please add product ");
            RuleForEach(x => x.Items).SetValidator(new ShoppingCartItemModelValidator());
            RuleFor(x => x).Must(x => x.Items.GroupBy(x => x.ProductId).Count() <= 1).WithMessage("Duplicate product");
        }
    }
}
