using G20.API.Factories.Media;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Categories;
using G20.API.Models.Venue;
using G20.API.Models.VenueTicketCategoriesMap;
using G20.Core.Domain;
using G20.Service.Countries;
using G20.Service.TicketCategories;
using G20.Service.Venues;
using G20.Service.VenueTicketCategoriesMap;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Factories.Venues
{
    public class VenueFactoryModel : IVenueFactoryModel
    {
        protected readonly IVenueService _venueService;
        protected readonly ICountryService _countryService;
        protected readonly IVenueTicketCategoryMapService _venueTicketCategoryMapService;
        protected readonly IMediaFactoryModel _mediaFactoryModel;
        protected readonly ITicketCategoryService _ticketCategoryService;

        public VenueFactoryModel(IVenueService venueService
            , ICountryService countryService
            , IVenueTicketCategoryMapService venueTicketCategoryMapService
            , IMediaFactoryModel mediaFactoryModel
            , ITicketCategoryService ticketCategoryService
            )
        {
            _venueService = venueService;
            _countryService = countryService;
            _venueTicketCategoryMapService = venueTicketCategoryMapService;
            _mediaFactoryModel = mediaFactoryModel;
            _ticketCategoryService = ticketCategoryService;
        }

        public virtual async Task<VenueListModel> PrepareVenueListModelAsync(VenueSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var venues = await _venueService.GetVenueAsync(name: searchModel.StadiumName,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new VenueListModel().PrepareToGridAsync(searchModel, venues, () =>
            {
                return venues.SelectAwait(async venue =>
                {
                    var venueModel = venue.ToModel<VenueModel>();
                    venueModel.CountryName = (await _countryService.GetByIdAsync(venue.CountryId))?.Name;
                    return venueModel;
                });
            });

            return model;
        }

        public virtual async Task<VenueModel> PrepareVenueModelAsync(Venue entity, bool isDetail = false)
        {
            return entity.ToModel<VenueModel>();
        }

        public virtual async Task<List<VenueTicketCategoryMapModel>> PrepareVenueTicketCategoryMapListModelAsync(int venueId)
        {
            List<VenueTicketCategoryMapModel> venueTicketCategoryMapModels = new List<VenueTicketCategoryMapModel>();
            var venueTicketCategoryMaps = await _venueTicketCategoryMapService.GetVenueTicketCategoryMapsByVenueIdAsync(venueId);
            var ticketCategories = (await _ticketCategoryService.GetTicketCategoryAsync(string.Empty)).ToList();
            List<VenueTicketCategoryMapModel> venueTicketCategoryMapsModel = new List<VenueTicketCategoryMapModel>();
            foreach (var venueTicketCategoryMap in venueTicketCategoryMaps)
            {
                var ticketCategory = ticketCategories.FirstOrDefault(x => x.Id == venueTicketCategoryMap.TicketCategoryId);
                VenueTicketCategoryMapModel model = new VenueTicketCategoryMapModel();
                model.Id = venueTicketCategoryMap.Id;
                model.VenueId = venueTicketCategoryMap.Id;
                if (ticketCategory != null)
                {
                    model.TicketCategoryId = ticketCategory.Id;
                    model.TicketCategoryName = ticketCategory.Name;
                    model.File = await _mediaFactoryModel.GetRequestModelAsync(ticketCategory.FileId);
                    model.Capacity = venueTicketCategoryMap.Capacity;
                    model.Amount = venueTicketCategoryMap.Amount;
                }
                venueTicketCategoryMapsModel.Add(model);
            }
            return venueTicketCategoryMapsModel;
        }
    }
}
