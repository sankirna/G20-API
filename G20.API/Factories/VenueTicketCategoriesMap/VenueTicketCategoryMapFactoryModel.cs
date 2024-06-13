using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.VenueTicketCategoriesMap;
using G20.Service.VenueTicketCategoriesMap;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Factories.VenueTicketCategoriesMap
{
    public class VenueTicketCategoryMapFactoryModel : IVenueTicketCategoryMapFactoryModel
    {
        protected readonly IVenueTicketCategoryMapService _service;
        public VenueTicketCategoryMapFactoryModel(IVenueTicketCategoryMapService service)
        {
            _service = service;
        }

        public virtual async Task<VenueTicketCategoryMapListModel> PrepareVenueTicketCategoryMapListModelAsync(VenueTicketCategoryMapSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var maps = await _service.GetVenueTicketCategoryMapsAsync(  searchModel.Page - 1, searchModel.PageSize);

            var model = await new VenueTicketCategoryMapListModel().PrepareToGridAsync(searchModel, maps, () =>
            {
                return maps.SelectAwait(async map =>
                {
                    var mapModel = map.ToModel<VenueTicketCategoryMapModel>();
                    return mapModel;
                });
            });

            return model;
        }
    }
}
