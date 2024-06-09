using FluentValidation;
using G20.API.Models;

namespace G20.API.Validators
{
    public class TestValidator : AbstractValidator<TestRequestModel>
    {
        public TestValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty()
                .WithMessage("Name is required");
            RuleFor(model => model.Email)
                .NotEmpty()
                .WithMessage("Name is required");
        }
    }
}
