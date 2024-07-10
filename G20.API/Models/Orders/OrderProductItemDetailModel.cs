using G20.API.Models.Media;
using G20.API.Models.Products;
using G20.Framework.Models;

namespace G20.API.Models.Orders
{
    public partial record OrderProductItemDetailModel : BaseNopEntityModel
    {
        public int UserId { get; set; }
        public int OrderProductItemId { get; set; }
        public int? ProductComboId { get; set; }
        public int ProductId { get; set; }
        public string QRCode { get; set; }
        public int? QRCodeFileId { get; set; }
        public ProductModel ProductDetail { get; set; }
        public FileUploadRequestModel? QRCodeFile { get; set; }
    }
}
