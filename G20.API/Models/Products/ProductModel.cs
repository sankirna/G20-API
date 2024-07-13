using G20.API.Models.Categories;
using G20.API.Models.Media;
using G20.API.Models.ProductCombos;
using G20.API.Models.ProductTicketCategoriesMap;
using G20.API.Models.Teams;
using G20.API.Models.Venue;
using G20.Core.Enums;
using G20.Framework.Models;

namespace G20.API.Models.Products
{
    public partial record ProductModel : BaseNopEntityModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
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
        public bool IsOutOfStock
        {
            get
            {
                return ProductTicketCategories != null && ProductTicketCategories.All(x => x.IsOutOfStock);
            }
        }
        public FileUploadRequestModel? File { get; set; }
        public TeamModel Team1Detail { get; set; }
        public TeamModel Team2Detail { get; set; }
        public VenueModel VenueDetail { get; set; }
        public CategoryModel CategoryDetail { get; set; }
        public List<ProductTicketCategoryMapModel> ProductTicketCategories { get; set; }
        public List<ProductComboModel> ProductCombos { get; set; }
        public List<ProductModel> ProductComboDetails { get; set; }
    }
}
