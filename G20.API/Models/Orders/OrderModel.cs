using G20.Framework.Models;

namespace G20.API.Models.Orders
{
    public partial record OrderModel : BaseNopEntityModel
    {
        public string CouponCode { get; set; }
        public int? CouponId { get; set; }
        public decimal TotalQuantity
        {
            get
            {
                return Items == null ? 0 : Items.Sum(x => x.Quantity);
            }
        }
        public decimal GrossTotal
        {
            get
            {
                return Items == null ? 0 : Items.Sum(x => x.Total);
            }
        }
        public decimal? Discount { get; set; }
        public decimal GrandTotal
        {
            get
            {
                return Discount.HasValue ? (GrossTotal - Discount.Value) : GrossTotal;
            }
        }

        public List<OrderProductItemModel> Items { get; set; }
    }
}
