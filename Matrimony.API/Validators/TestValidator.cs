using FluentValidation;
using Matrimony.API.Models;

namespace Matrimony.API.Validators
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
