using G20.Framework.Models;
using Nop.Web.Framework.Models;

namespace G20.API.Models.SubCategories
{
    public partial record SubCategoryModel : BaseNopEntityModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
