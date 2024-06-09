using Nop.Web.Framework.Models;

namespace G20.API.Models.Families
{
    public partial record FamilySearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
