using Nop.Web.Framework.Models;

namespace G20.API.Models.Categories
{
    public partial record CategorySearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
