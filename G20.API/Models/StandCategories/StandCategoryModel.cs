using G20.Framework.Models;
using Nop.Web.Framework.Models;

namespace G20.API.Models.StandCategories
{
    public partial record StandCategoryModel : BaseNopEntityModel
    {
        public string StandName { get; set; }
        public int VenueId { get; set; }
        public int Capacity { get; set; }
        public string EntryGate { get; set; }
        public string Facilities { get; set; }
    }
}
