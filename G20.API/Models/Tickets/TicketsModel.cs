using G20.API.Models.Products;
using G20.Framework.Models;

namespace G20.API.Models.Tickets
{
    public partial record TicketsModel : BaseNopEntityModel
    {
        public ProductModel Product { get; set; }
        public int TicketCategoryId { get; set; }
        public int TicketsIssue { get; set;}
        public int Available { get; set;}
        public int Blocked { get; set;}
        public int Sold { get; set;}
        public decimal TicketPrice { get; set;}
    }
}
