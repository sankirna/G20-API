using Nop.Web.Framework.Models;

namespace G20.API.Models.ProductTicketCategoriesMap
{
    public partial record ProductTicketCategoryMapSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
