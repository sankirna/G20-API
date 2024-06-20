
namespace G20.Core.Domain
{
    public partial class ShoppingCart : BaseEntityWithTacking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CouponCode { get; set; }
        public int? CouponId { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal GrandTotal { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Coupon Coupon { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
