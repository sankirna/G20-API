using Matrimony.Framework.Models;

namespace Matrimony.API.Models
{
    public partial record TestResponseModel : BaseNopEntityModel
    {
        public string Name { get; set; }
    }
}
