using G20.Framework.Models;

namespace G20.API.Models
{

    public partial record TestRequestModel : BaseNopEntityModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
