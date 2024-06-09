using Matrimony.Framework.Models;
using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.Occupations
{
    public partial record OccupationModel : BaseNopEntityModel
    {
        public int ProfileId { get; set; }

        public string Name { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsPresent { get; set; }

        public int TypeId { get; set; }

        public string? Description { get; set; }
    }
}
