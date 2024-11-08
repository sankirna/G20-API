using G20.API.Models.Countries;
using G20.Framework.Models;

namespace G20.API.Models.Sponsors
{
    public partial record SponsorModel : BaseNopEntityModel
    {
        public int SponsorId { get; set; }        
        public int ProductId { get; set; }
        public int MatchId { get; set; }
        public int AllocatedTickets { get; set; }
        public DateTime AllocatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int AllocatedBy{ get; set; }
    }
}
