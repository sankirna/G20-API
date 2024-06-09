using G20.Framework.Models;
using Nop.Web.Framework.Models;

namespace G20.API.Models.Countries
{
    public partial record CountryModel : BaseNopEntityModel
    {
        public string Name { get; set; } = null!;
    }
}
