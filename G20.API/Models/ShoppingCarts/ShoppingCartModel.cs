using G20.Framework.Models;

namespace G20.API.Models.ShoppingCarts
{
    public partial record ShoppingCartModel : BaseNopEntityModel
    {
        public string CouponCode { get; set; }
        public int? CouponId { get; set; }
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

        public virtual List<ShoppingCartItemModel> Items { get; set; } = new List<ShoppingCartItemModel>();

    }
}
