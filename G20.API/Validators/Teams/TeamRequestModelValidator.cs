using FluentValidation;
using G20.API.Models.Teams;

namespace G20.API.Validators.Teams
{
    public class TeamRequestModelValidator : AbstractValidator<TeamRequestModel>
    {
        public TeamRequestModelValidator()
        {
            //RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            //RuleFor(x => x.Coach).NotEmpty().WithMessage("Coach is required.");
            //RuleFor(x => x.HomeCity).NotEmpty().WithMessage("Home City is required.");
        }
    }
}
