using Nop.Web.Framework.Models;

namespace G20.API.Models.Achivements
{
    public partial record AchivementSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
