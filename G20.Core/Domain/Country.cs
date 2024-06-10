using System;
using System.Collections.Generic;

namespace G20.Core.Domain;

public partial class Country : BaseEntityWithTacking
{

    public string Name { get; set; } = null!;
    public bool? IsDeleted { get; set; }

    public virtual ICollection<State> States { get; set; } = new List<State>();

}
