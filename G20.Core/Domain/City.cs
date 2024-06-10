using System;
using System.Collections.Generic;

namespace G20.Core.Domain;

public partial class City : BaseEntity
{

    public string Name { get; set; } = null!;

    public int? StateId { get; set; }

    public virtual State? State { get; set; }

}
