using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.Achivements
{
    public partial record AchivementSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
