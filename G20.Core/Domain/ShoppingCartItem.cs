

namespace G20.Core.Domain
{
    public partial class ShoppingCartItem : BaseEntityWithTacking
    {
        public int UserId { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int ProductTicketCategoryMapId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public Product Product { get; set; }
        public ProductTicketCategoryMap ProductTicketCategoryMap { get; set; }
    }
}
