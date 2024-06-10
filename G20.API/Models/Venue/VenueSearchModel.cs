using Nop.Web.Framework.Models;

namespace G20.API.Models.Venue
{
    public partial record VenueSearchModel : BaseSearchModel
    {
        public string StadiumName { get; set; }
    }
}
