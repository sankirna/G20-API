using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Venue;
using G20.Service.Venue;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Factories.Venue
{
    public class VenueFactoryModel : IVenueFactoryModel
    {
        protected readonly IVenueService _venueService;

        public VenueFactoryModel(IVenueService venueService)
        {
            _venueService = venueService;
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
                    return venueModel;
                });
            });

            return model;
        }
    }
}
