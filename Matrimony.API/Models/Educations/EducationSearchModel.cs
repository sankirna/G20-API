using Nop.Web.Framework.Models;

namespace G20.API.Models.Achivements
{
    public partial record EducationSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
