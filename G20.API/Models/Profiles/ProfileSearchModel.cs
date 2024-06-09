using Nop.Web.Framework.Models;

namespace G20.API.Models.Profiles
{
    public partial record ProfileSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
