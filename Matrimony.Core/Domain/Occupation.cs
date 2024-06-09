using System;
using System.Collections.Generic;

namespace Matrimony.Core.Domain;

public partial class Occupation : BaseEntity
{
    public int ProfileId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsPresent { get; set; }

    public int TypeId { get; set; }

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public int? DisplayOrder { get; set; }
}
