using G20.Framework.Models;

namespace G20.API.Models
{
    public partial record TestResponseModel : BaseNopEntityModel
    {
        public string Name { get; set; }
    }
}
