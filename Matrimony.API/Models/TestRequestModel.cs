using Matrimony.Framework.Models;

namespace Matrimony.API.Models
{

    public partial record TestRequestModel : BaseNopEntityModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
