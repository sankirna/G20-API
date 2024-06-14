using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Core.Domain
{
    public partial class ProductCombo : BaseEntityWithTacking
    {
        public int ProductId { get; set; }
        public int ProductMapId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Product? ProductMap { get; set; }
    }
}
