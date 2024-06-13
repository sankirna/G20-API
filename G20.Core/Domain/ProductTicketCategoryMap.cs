namespace G20.Core.Domain
{
    public partial class ProductTicketCategoryMap : BaseEntityWithTacking
    {
        public int ProductId { get; set; }
        public int TicketCategoryId { get; set; }
        public int Total { get; set; }
        public int Available { get; set; }
        public int Block { get; set; }
        public int Sold { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Product? Product { get; set; }
        public virtual TicketCategory? TicketCategory { get; set; }
    }
}
