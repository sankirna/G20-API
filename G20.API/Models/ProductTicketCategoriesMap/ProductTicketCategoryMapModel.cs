using G20.API.Models.Media;
using G20.Framework.Models;

namespace G20.API.Models.ProductTicketCategoriesMap
{
    public partial record ProductTicketCategoryMapModel : BaseNopEntityModel
    {
        public int ProductId { get; set; }
        public int TicketCategoryId { get; set; }
        public string TicketCategoryName { get; set; }
        public int Total { get; set; }
        public int Available { get; set; }
        public int Block { get; set; }
        public int Sold { get; set; }
        public decimal Price { get; set; }
        public FileUploadRequestModel? File { get; set; }
    }
}
