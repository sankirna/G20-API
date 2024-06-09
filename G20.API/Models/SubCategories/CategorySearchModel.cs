using Nop.Web.Framework.Models;

namespace G20.API.Models.SubCategories
{
    public partial record SubCategorySearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
