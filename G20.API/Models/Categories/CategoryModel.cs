using G20.Framework.Models;
using Nop.Web.Framework.Models;

namespace G20.API.Models.Categories
{
    public partial record CategoryModel : BaseNopEntityModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
