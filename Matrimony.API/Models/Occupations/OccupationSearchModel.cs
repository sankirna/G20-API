using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.Occupations
{
    public partial record OccupationSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
