using G20.Core.Domain;
using System;
using System.Collections.Generic;

namespace G20.Core.Domain;

public partial class File : BaseEntityWithTacking
{
    public string? Name { get; set; }
    public string? OriginalName { get; set; }
    public int TypeId { get; set; }
    public bool? IsDeleted { get; set; }
}
