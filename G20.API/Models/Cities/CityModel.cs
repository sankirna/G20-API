using G20.API.Models.States;
using G20.Framework.Models;


namespace G20.API.Models.Cities
{
    public partial record CityModel : BaseNopEntityModel
    {
        public string Name { get; set; } = null!;
        public StateModel State { get; set; }
        public int StateId { get; set; }
    }
}

