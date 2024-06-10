using System;
using System.Collections.Generic;

namespace G20.Core.Domain;

public partial class State : BaseEntityWithTacking
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? CountryId { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Country? Country { get; set; }

}
