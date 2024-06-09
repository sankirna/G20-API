using Matrimony.Framework.Models;
using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.Educations
{
    public partial record EducationModel : BaseNopEntityModel
    {
        public int ProfileId { get; set; }

        public string Name { get; set; } = null!;

        public int StartYear { get; set; }

        public int StartMonth { get; set; }

        public int? EndYear { get; set; }

        public int EndMonth { get; set; }

        public bool IsPresent { get; set; }

        public string? Grade { get; set; }

        public int? Percentage { get; set; }

        public string? Degree { get; set; }

        public string? Description { get; set; }
    }
}
