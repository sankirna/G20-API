

namespace G20.Core.Domain
{
    public partial class BoardingDetail : BaseEntityWithTacking
    {
        public int UserId { get; set; }
        public int OrderProductItemDetailId { get; set; }
        public DateTime EntryDateTime { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public OrderProductItemDetail OrderProductItemDetail { get; set; }
    }
}
