using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.Countries
{
    public partial record CountrySearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
