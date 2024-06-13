using G20.API.Models.Media;
using G20.API.Models.Tickets;
using G20.Framework.Models;

namespace G20.API.Models.Products
{
    public partial record ProductModel : BaseNopEntityModel
    {
        public int ProductTypeId { get; set; }
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
        public int VenueId { get; set; }
        public DateTime MatchDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Description { get; set; }

        public int? FileId { get; set; }
        public FileUploadRequestModel? File { get; set; }

        public List<TicketsModel> ListTickets { get; set; }
    }
}
