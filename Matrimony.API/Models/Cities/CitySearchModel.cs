using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.Cities
{
    public partial record CitySearchModel : BaseSearchModel
    {
        public string Name { get; set; }
        public int StateId { get; set; }
    }
}
