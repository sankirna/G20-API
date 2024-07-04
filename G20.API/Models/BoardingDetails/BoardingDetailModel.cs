using G20.Framework.Models;

namespace G20.API.Models.BoardingDetails
{
    public partial record BoardingDetailModel : BaseNopEntityModel
    {
        public int UserId { get; set; }
        public int OrderProductItemDetailId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int TotalQuantity { get; set; }
        public int Quantity { get; set; }
    }
}
