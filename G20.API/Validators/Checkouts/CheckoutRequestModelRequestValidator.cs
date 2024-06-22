using FluentValidation;
using G20.API.Models.Checkout;
using G20.API.Models.Venue;

namespace G20.API.Validators.Checkouts
{
    public class CheckoutRequestModelRequestValidator : AbstractValidator<CheckoutRequestModel>
    {
        public CheckoutRequestModelRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required ");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required ");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required ");
        }
    }
}