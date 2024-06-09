using Nop.Web.Framework.Models;

namespace G20.API.Models.Occupations
{
    public partial record OccupationSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
