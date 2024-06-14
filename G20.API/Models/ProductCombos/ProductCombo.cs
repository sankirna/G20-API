using G20.Framework.Models;

namespace G20.API.Models.ProductCombos
{
    public partial record ProductComboModel : BaseNopEntityModel
    {
        public int ProductId { get; set; }
        public int ProductMapId { get; set; }
    }
}
