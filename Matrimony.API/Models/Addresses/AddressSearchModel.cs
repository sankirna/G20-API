using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.Addresss
{
    public partial record AddressSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
