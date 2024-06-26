using Nop.Web.Framework.Models;

namespace G20.API.Models.MessageTemplates
{
    public partial record MessageTemplateSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
    }
}
