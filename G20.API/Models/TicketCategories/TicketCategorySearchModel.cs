using Nop.Web.Framework.Models;

namespace G20.API.Models.TicketCategories
{
    public partial record TicketCategorySearchModel : BaseSearchModel
    {
        public string StandName { get; set; }
    }
}
