using Nop.Web.Framework.Models;

namespace Matrimony.API.Models.States
{
    public partial record StateSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
        public int CounryId { get; set; }
    }
}