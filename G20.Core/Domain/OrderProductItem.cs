

using G20.Core.Enums;

namespace G20.Core.Domain
{
    public partial class OrderProductItem : BaseEntityWithTacking
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductTicketCategoryMapId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public ProductTypeEnum ProductType { get; set; }
        public ProductTicketCategoryMap ProductTicketCategoryMap { get; set; }
        public ICollection<OrderProductItemDetail> OrderProductItemDetails { get; set; }
    }
}
