using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.Profiles
{
    public partial record ProfileSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
