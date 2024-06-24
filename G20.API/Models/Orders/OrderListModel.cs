using Nop.Web.Framework.Models;

namespace G20.API.Models.Orders
{
    public partial record OrderListModel : BasePagedListModel<OrderDetailModel>
    {
    }
}
