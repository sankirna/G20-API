
namespace G20.Core.Domain
{
    public partial class Venue : BaseEntityWithTacking
    {
        public string StadiumName { get; set; } 
        public string Location { get; set; }
        public int CountryId { get; set; }
        public int Capacity { get; set; }
        public bool IsDeleted { get; set; }
    }
}
