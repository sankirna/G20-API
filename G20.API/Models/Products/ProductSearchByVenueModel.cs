using Nop.Web.Framework.Models;

namespace G20.API.Models.Products
{
    public partial record ProductSearchByVenueModel : BaseSearchModel
    {
        public int VenueId { get; set; }        
    }

    
}
