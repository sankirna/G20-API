using AutoMapper;
using G20.API.Factories.Media;
using G20.API.Factories.Venue;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Venue;
using G20.API.Models.VenueTicketCategoriesMap;
using G20.Core;
using G20.Core.Domain;
using G20.Service.TicketCategories;
using G20.Service.Venue;
using G20.Service.VenueTicketCategoriesMap;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;

namespace G20.API.Controllers
{
    public class VenueController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IVenueFactoryModel _venueFactoryModel;
        protected readonly IMediaFactoryModel _mediaFactoryModel;
        protected readonly IVenueService _venueService;
        protected readonly ITicketCategoryService _ticketCategoryService;
        protected readonly IVenueTicketCategoryMapService _venueTicketCategoryMapService;


        public VenueController(IWorkContext workContext
            , IVenueFactoryModel venueFactoryModel
            , IMediaFactoryModel mediaFactoryModel
            , IVenueService venueService
            , ITicketCategoryService ticketCategoryService
            , IVenueTicketCategoryMapService venueTicketCategoryMapService
            )
        {
            _workContext = workContext;
            _venueFactoryModel = venueFactoryModel;
            _mediaFactoryModel = mediaFactoryModel;
            _venueService = venueService;
            _ticketCategoryService = ticketCategoryService;
            _venueTicketCategoryMapService = venueTicketCategoryMapService;
        }

        #region Private Method

        private async Task AddUpdateVenueTicketCategoryMapModels(int venueId, List<VenueTicketCategoryMapModel> venueTicketCategoryMapsModel)
        {
            if (venueTicketCategoryMapsModel != null)
            {
                venueTicketCategoryMapsModel.ForEach(x => { x.VenueId = venueId; });
                var venueTicketCategoryMaps = await _venueTicketCategoryMapService.GetVenueTicketCategoryMapsByVenueIdAsync(venueId);
                var existingIds = venueTicketCategoryMaps.Select(x => x.Id);
                var requestIds = venueTicketCategoryMapsModel.Select(x => x.Id);
                var updateIds = requestIds.Intersect(existingIds);
                var deleteIds = existingIds.Except(requestIds);
                var addedIds = requestIds.Except(existingIds);

                var deleteTicketCategoryMaps = venueTicketCategoryMaps.Where(x => deleteIds.Contains(x.Id));
                foreach (var ticketCategoryMap in deleteTicketCategoryMaps)
                {
                    await _venueTicketCategoryMapService.DeleteAsync(ticketCategoryMap);
                }

                var updateTicketCategoryMaps = venueTicketCategoryMapsModel.Where(x => updateIds.Contains(x.Id));
                foreach (var ticketCategoryMapRequest in updateTicketCategoryMaps)
                {
                    var ticketCategoryMap = venueTicketCategoryMaps.FirstOrDefault(x => x.Id == ticketCategoryMapRequest.Id);
                    if (ticketCategoryMap == null)
                        throw new NopException("ticketCategoryMap not found");
                    ticketCategoryMapRequest.VenueId = venueId;
                    ticketCategoryMap = ticketCategoryMapRequest.ToEntity(ticketCategoryMap);
                    await _venueTicketCategoryMapService.UpdateAsync(ticketCategoryMap);
                }

                var addTicketCategoryMaps = venueTicketCategoryMapsModel.Where(x => addedIds.Contains(x.Id));
                foreach (var ticketCategoryMapRequest in addTicketCategoryMaps)
                {
                    ticketCategoryMapRequest.VenueId = venueId;
                    var ticketCategoryMap = ticketCategoryMapRequest.ToEntity<VenueTicketCategoryMap>();
                    await _venueTicketCategoryMapService.InsertAsync(ticketCategoryMap);
                }
            }
        }

        #endregion

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
            var model = venue.ToModel<VenueModel>();
            model.VenueTicketCategories = await _venueFactoryModel.PrepareVenueTicketCategoryMapListModelAsync(id);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(VenueModel model)
        {
            var venue = model.ToEntity<Venue>();
            await _venueService.InsertAsync(venue);
            var entityUpdatedModel = venue.ToModel<VenueModel>();
            await AddUpdateVenueTicketCategoryMapModels(entityUpdatedModel.Id, model.VenueTicketCategories);
            return Success(entityUpdatedModel);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(VenueModel model)
        {
            var venue = await _venueService.GetByIdAsync(model.Id);
            if (venue == null)
                return Error("not found");

            venue = model.ToEntity(venue);
            await _venueService.UpdateAsync(venue);
            await AddUpdateVenueTicketCategoryMapModels(model.Id, model.VenueTicketCategories);
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
