using G20.API.Models.Products;
using G20.API.Models.ProductTicketCategoriesMap;
using G20.Framework.Models;

namespace G20.API.Models.ShoppingCarts
{
    public partial record ShoppingCartItemModel : BaseNopEntityModel
    {
        public int ProductId { get; set; }
        public int ProductTicketCategoryMapId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Quantity * Price; } }

        public ProductModel ProductDetail { get; set; }
        public ProductTicketCategoryMapModel ProductTicketCategoryMapDetail { get; set; }
    }
}
