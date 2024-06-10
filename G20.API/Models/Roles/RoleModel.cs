using G20.Framework.Models;

namespace G20.API.Models.Roles
{
    public partial record RoleModel : BaseNopEntityModel
    {
        public string Name { get; set; } = null!;
    }
}
