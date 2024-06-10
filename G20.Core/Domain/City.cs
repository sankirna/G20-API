using System.Collections.Generic;

namespace G20.Core.Domain;

public partial class City : BaseEntityWithTacking
{
    public string Name { get; set; } = null!;
    public int? StateId { get; set; }
    public bool? IsDeleted { get; set; }
    public virtual State? State { get; set; }

}
