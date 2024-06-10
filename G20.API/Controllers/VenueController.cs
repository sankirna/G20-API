using G20.API.Factories.Venue;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Venue;
using G20.Core;
using G20.Core.Domain;
using G20.Service.Venue;
using Microsoft.AspNetCore.Mvc;

namespace G20.API.Controllers
{
    public class VenueController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IVenueFactoryModel _venueFactoryModel;
        protected readonly IVenueService _venueService;


        public VenueController(IWorkContext workContext, IVenueFactoryModel venueFactoryModel, IVenueService venueService)
        {
            _workContext = workContext;
            _venueFactoryModel = venueFactoryModel;
            _venueService = venueService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(VenueSearchModel searchModel)
        {
            var model = await _venueFactoryModel.PrepareVenueListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var venue = await _venueService.GetByIdAsync(id);
            if (venue == null)
                return Error("not found");
            return Success(venue.ToModel<VenueModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(VenueModel model)
        {
            var venue = model.ToEntity<Venues>();
            await _venueService.InsertAsync(venue);
            return Success(venue.ToModel<VenueModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(VenueModel model)
        {
            var venue = await _venueService.GetByIdAsync(model.Id);
            if (venue == null)
                return Error("not found");

            venue = model.ToEntity(venue);
            await _venueService.UpdateAsync(venue);
            return Success(venue.ToModel<VenueModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var venue = await _venueService.GetByIdAsync(id);
            if (venue == null)
                return Error("not found");
            await _venueService.DeleteAsync(venue);
            return Success(id);
        }
    }
}
