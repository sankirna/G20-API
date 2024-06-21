using G20.Framework.Models;

namespace G20.API.Models.ShoppingCarts
{
    public partial record ShoppingCartItemModel : BaseNopEntityModel
    {
        public int ProductId { get; set; }
        public int ProductTicketCategoryMapId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get { return Quantity * Price; } }
    }
}
