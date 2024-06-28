using G20.API.Models.Products;
using G20.API.Models.ProductTicketCategoriesMap;
using G20.API.Models.Users;
using G20.Core.Domain;
using G20.Framework.Models;
using Humanizer;

namespace G20.API.Models.Orders
{ 
    public partial record UserProductItemDetail : BaseNopEntityModel
    {  
        public int UserId { get; set; }
        public int OrderProductItemDetailId { get; set; }
        public string UserName { get; set; }
        public int TotalQuantity {  get; set; }
        public int RemainingQuantity {  get; set; }
        public string StandName { get; set; }
    }
}
