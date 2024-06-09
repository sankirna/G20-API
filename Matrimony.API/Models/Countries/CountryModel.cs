using Matrimony.Framework.Models;
using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.Countries
{
    public partial record CountryModel : BaseNopEntityModel
    {
        public string Name { get; set; } = null!;
    }
}
