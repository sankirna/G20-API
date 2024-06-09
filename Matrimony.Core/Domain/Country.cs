using System;
using System.Collections.Generic;

namespace Matrimony.Core.Domain;

public partial class Country : BaseEntity
{

    public string Name { get; set; } = null!;

    public virtual ICollection<State> States { get; set; } = new List<State>();

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
