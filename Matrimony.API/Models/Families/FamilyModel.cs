using Matrimony.Framework.Models;
using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.Families
{
    public partial record FamilyModel : BaseNopEntityModel
    {
        public int ProfileId { get; set; }

        public string Name { get; set; } = null!;

        public int RelationTypeId { get; set; }

        public string? Email { get; set; }

        public string? PhoneNo { get; set; }

        public string? Description { get; set; }
    }
}
