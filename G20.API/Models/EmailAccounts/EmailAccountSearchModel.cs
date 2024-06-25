using Nop.Web.Framework.Models;

namespace G20.API.Models.EmailAccounts
{
    public partial record EmailAccountSearchModel : BaseSearchModel
    {
        public string Email { get; set; }
    }
}
