using Nop.Web.Framework.Models;

namespace G20.API.Models.States
{
    public partial record StateSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
        public int CounryId { get; set; }
    }
}