using G20.Core.Enums;

namespace G20.API.Models.Checkout
{
    public partial record CheckoutRequestModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public PaymentTypeEnum PaymentTypeId { get; set; }
    }
}
