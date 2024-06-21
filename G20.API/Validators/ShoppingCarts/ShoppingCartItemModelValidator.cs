using FluentValidation;
using G20.API.Models.ShoppingCarts;

namespace G20.API.Validators.ShoppingCarts
{
    public class ShoppingCartItemModelValidator : AbstractValidator<ShoppingCartItemModel>
    {
        public ShoppingCartItemModelValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Invalid product");
            RuleFor(x => x.ProductTicketCategoryMapId).GreaterThan(0).WithMessage("Invalid product ticket category");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity should be greater than zero");
        }
    }
}
