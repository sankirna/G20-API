using G20.API.Models.Countries;
using G20.Framework.Models;

namespace G20.API.Models.States
{
    public partial record StateModel : BaseNopEntityModel
    {
        public string Name { get; set; } = null!;
        public CountryModel Country { get; set; }
        public int CountryId { get; set; }
    }
}
