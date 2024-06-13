using Nop.Web.Framework.Models;

namespace G20.API.Models.Products
{
    public partial record ProductSearchModel : BaseSearchModel
    {
        public string Team { get; set; }
    }
}
