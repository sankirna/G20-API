using Nop.Web.Framework.Models;

namespace G20.API.Models.StandCategories
{
    public partial record StandCategorySearchModel : BaseSearchModel
    {
        public string StandName { get; set; }
    }
}
