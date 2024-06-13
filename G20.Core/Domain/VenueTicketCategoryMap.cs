namespace G20.Core.Domain
{
    public partial class VenueTicketCategoryMap : BaseEntityWithTacking
    {
        public int VenueId { get; set; }
        public int TicketCategoryId { get; set; }
        public int Capacity { get; set; }
        public decimal Amount { get; set; }
        public bool IsDeleted { get; set; }
        public virtual File? File { get; set; }

    }
}
