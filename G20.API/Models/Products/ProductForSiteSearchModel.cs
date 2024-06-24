using Nop.Web.Framework.Models;

namespace G20.API.Models.Products
{
    public partial record ProductForSiteSearchModel : BaseSearchModel
    {
        public string searchText { get; set; }
        public int? ProductTypeId { get; set; }
        public int? TeamId { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinimumPrice { get; set; } = 0;
        public decimal? MaximumPrice { get; set; } = 1000;
    }
}
