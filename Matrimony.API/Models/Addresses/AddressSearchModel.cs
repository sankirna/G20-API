using Nop.Web.Framework.Models;

namespace G20.API.Models.Addresss
{
    public partial record AddressSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
