using Nop.Web.Framework.Models;

namespace G20.API.Models.Countries
{
    public partial record CountrySearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
