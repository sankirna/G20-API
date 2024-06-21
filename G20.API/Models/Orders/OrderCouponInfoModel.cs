namespace G20.API.Models.Orders
{
    public partial record OrderCouponInfoModel
    {
        public decimal Discount { get; set; }
        public decimal CouponId { get; set; }
    }
}
