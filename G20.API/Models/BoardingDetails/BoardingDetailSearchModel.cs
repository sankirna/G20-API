using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.BoardingDetails
{
    public partial record BoardingDetailSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
