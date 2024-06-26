using G20.Core.Enums;
using Nop.Web.Framework.Models;

namespace G20.API.Models.Orders
{
    public partial record UserOrderHistoryRequestModel : BaseSearchModel
    {
        public int UserId { get; set; }
        public int? OrderStatusId { get; set; }
        public OrderStatusEnum? OrderStatusEnum
        {
            get
            {
                return OrderStatusId.HasValue ? (OrderStatusEnum)OrderStatusId.Value : null;
            }
        }
    }
}
