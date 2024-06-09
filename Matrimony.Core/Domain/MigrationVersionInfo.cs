using System;
using System.Collections.Generic;

namespace Matrimony.Core.Domain;

public partial class MigrationVersionInfo
{
    public long Version { get; set; }

    public DateTime? AppliedOn { get; set; }

    public string? Description { get; set; }
}
