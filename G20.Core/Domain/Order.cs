

namespace G20.Core.Domain
{
    public partial class Order : BaseEntityWithTacking
    {
        public int UserId { get; set; }
        public string? CouponCode { get; set; }
        public int? CouponId { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal GrandTotal { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int PaymentTypeId { get; set; }
        public int OrderStatusId { get; set; }
        public int PaymentStatusId { get; set; }
        public string POSTransactionId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Coupon Coupon { get; set; }
        public PaymentDetail Payment { get; set; }
        public ICollection<OrderProductItem> OrderProductItems { get; set; }
    }
}
