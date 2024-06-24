using G20.Core.Enums;
using Nop.Web.Framework.Models;

namespace G20.API.Models.Orders
{
    public partial record OrderListRequestModel : BaseSearchModel
    {
        public int userId { get; set; }
        public int? OrderStatusId { get; set; }
        public OrderStatusEnum? OrderStatusEnum
        {
            get
            {
                return OrderStatusId.HasValue ? (OrderStatusEnum)OrderStatusId.Value : null;
            }
        }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
