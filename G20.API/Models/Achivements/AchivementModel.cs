using G20.Framework.Models;
using Nop.Web.Framework.Models;

namespace G20.API.Models.Achivements
{
    public partial record AchivementModel : BaseNopEntityModel
    {
        public int Id { get; set; }

        public int ProfileId { get; set; }

        public string Name { get; set; } = null!;

        public int Year { get; set; }

        public string Description { get; set; } = null!;
    }
}
