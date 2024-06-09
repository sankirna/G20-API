using Matrimony.Framework.Models;
using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.Addresss
{
    public partial record AddressModel : BaseNopEntityModel
    {
        public int ProfileId { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public string? Landmark { get; set; }

        public int? CityId { get; set; }

        public int? StateId { get; set; }

        public int? CountryId { get; set; }

        public string? PinNo { get; set; }

        public int? TypeId { get; set; }

        public int? DisplayOrder { get; set; }
    }
}
