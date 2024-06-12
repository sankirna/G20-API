using FluentValidation;
using G20.API.Models.Venue;

namespace G20.API.Validators.VenueTicketCategoriesMap
{
    public class VenueTicketCategoryMapRequestModelValidator : AbstractValidator<VenueTicketCategoryMapRequestModel>
    {
        public VenueTicketCategoryMapRequestModelValidator()
        {
        }
    }
}
