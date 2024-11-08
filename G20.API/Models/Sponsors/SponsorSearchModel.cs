using Nop.Web.Framework.Models;

namespace G20.API.Models.Sponsors
{
    public partial record SponsorSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
        public int CounryId { get; set; }
    }
}