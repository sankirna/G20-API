using System;
using System.Collections.Generic;

namespace Matrimony.Core.Domain;

public partial class City : BaseEntity
{

    public string Name { get; set; } = null!;

    public int? StateId { get; set; }

    public virtual State? State { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
