using G20.API.Models.Media;
using G20.Framework.Models;

namespace G20.API.Models.VenueTicketCategoriesMap
{
    public partial record VenueTicketCategoryMapModel : BaseNopEntityModel
    {
        public int VenueId { get; set; }
        public int TicketCategoryId { get; set; }
        public string TicketCategoryName { get; set; }
        public int Capacity { get; set; }
        public decimal Amount { get; set; }
        public FileUploadRequestModel? File { get; set; }
    }
}
