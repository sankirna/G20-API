using G20.Framework.Models;
using Nop.Web.Framework.Models;

namespace G20.API.Models.Venue
{
    public partial record VenueModel : BaseNopEntityModel
    {
        public string StadiumName { get; set; }
        public string Location { get; set; }
        public int CountryId { get; set; }
        public string Capacity { get; set; }
    }
}
