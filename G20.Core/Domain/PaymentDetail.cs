using G20.Core.Enums;


namespace G20.Core.Domain
{
    public partial class PaymentDetail : BaseEntityWithTacking
    {
        public int TypeId { get; set; }
        public string TransactionId { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal GrandTotal { get; set; }

        // Navigation Properties
        public PaymentTypeEnum PaymentType { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
