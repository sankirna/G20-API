using Nop.Web.Framework.Models;

namespace G20.API.Models.Users
{
    public partial record UserSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
