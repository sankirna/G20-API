using Nop.Web.Framework.Models;

namespace G20.API.Models.Teams
{
    public partial record TeamSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
