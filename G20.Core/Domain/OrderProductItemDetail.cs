

namespace G20.Core.Domain
{
    public partial class OrderProductItemDetail : BaseEntityWithTacking
    {
        public int UserId { get; set; }
        public int OrderProductItemId { get; set; }
        public int? ProductComboId { get; set; }
        public int ProductId { get; set; }
        public string QRCode { get; set; }
        public int? QRCodeFileId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public OrderProductItem OrderProductItem { get; set; }
        public ProductCombo ProductCombo { get; set; }
        public Product Product { get; set; }
        public File QRCodeFile { get; set; }
        public ICollection<BoardingDetail> BoardingDetails { get; set; }
    }
}
