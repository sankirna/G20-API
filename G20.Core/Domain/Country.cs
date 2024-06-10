using System;
using System.Collections.Generic;

namespace G20.Core.Domain;

public partial class Country : BaseEntity
{

    public string Name { get; set; } = null!;

    public virtual ICollection<State> States { get; set; } = new List<State>();

}
