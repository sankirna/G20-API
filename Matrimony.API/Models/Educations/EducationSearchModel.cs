using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.Achivements
{
    public partial record EducationSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
