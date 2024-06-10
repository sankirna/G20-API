
namespace G20.Core.Domain
{
    public partial class Venue : BaseEntityWithTacking
    {
        public string StadiumName { get; set; } 
        public string Location { get; set; }
        public int CountryId { get; set; }
        public string Capacity { get; set; }
    }
}
