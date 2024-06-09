using Nop.Web.Framework.Models;

namespace G20.API.Models.Cities
{
    public partial record CitySearchModel : BaseSearchModel
    {
        public string Name { get; set; }
        public int StateId { get; set; }
    }
}
