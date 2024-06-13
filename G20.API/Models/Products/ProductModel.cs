using G20.API.Models.Media;
using G20.API.Models.ProductTicketCategoriesMap;
using G20.API.Models.Tickets;
using G20.API.Models.VenueTicketCategoriesMap;
using G20.Core.Enums;
using G20.Framework.Models;

namespace G20.API.Models.Products
{
    public partial record ProductModel : BaseNopEntityModel
    {
        public string Name { get; set; }
        public int ProductTypeId { get; set; }
        public ProductTypeEnum ProductTypeEnum { get { return (ProductTypeEnum)ProductTypeId; } }
        public int? Team1Id { get; set; }
        public int? Team2Id { get; set; }
        public int? VenueId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public DateTime? ScheduleDateTime { get; set; }
        public string Description { get; set; }
        public int? FileId { get; set; }
        public FileUploadRequestModel? File { get; set; }
        public List<ProductTicketCategoryMapModel> ProductTicketCategories { get; set; }

    }
}
