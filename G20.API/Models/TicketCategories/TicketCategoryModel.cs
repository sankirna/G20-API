using G20.API.Models.Media;
using G20.Framework.Models;
using Nop.Web.Framework.Models;

namespace G20.API.Models.TicketCategories
{
    public partial record TicketCategoryModel : BaseNopEntityModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? FileId { get; set; }
        public FileUploadRequestModel? File { get; set; }
    }
}
