using Nop.Web.Framework.Models;

namespace G20.API.Models.VenueTicketCategoriesMap
{
    public partial record VenueTicketCategoryMapSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
