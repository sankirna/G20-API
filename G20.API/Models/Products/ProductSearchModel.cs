using Nop.Web.Framework.Models;

namespace G20.API.Models.Products
{
    public partial record ProductSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
        public int? ProductTypeId { get; set; }
    }

    
}
