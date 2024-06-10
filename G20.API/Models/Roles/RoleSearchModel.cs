using Nop.Web.Framework.Models;

namespace G20.API.Models.Roles
{
    public partial record RoleSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
