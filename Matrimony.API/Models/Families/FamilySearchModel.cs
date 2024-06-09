using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.Families
{
    public partial record FamilySearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
