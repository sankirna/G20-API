using G20.Framework.Models;

namespace G20.API.Models.Orders
{
    public partial record OrderProductItemModel : BaseNopEntityModel
    {
        public int ProductId { get; set; }
        public int ProductTicketCategoryMapId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
