namespace G20.API.Models.Orders
{
    public partial record OrderCouponInfoModel
    {
        public decimal Discount { get; set; }
        public int? CouponId { get; set; }
    }
}
