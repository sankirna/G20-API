using Matrimony.Core.Domain;
using System;
using System.Collections.Generic;

namespace Matrimony.Core.Domain;

public partial class Education : BaseEntity
{
    public int ProfileId { get; set; }

    public string Name { get; set; } = null!;

    public int StartYear { get; set; }

    public int StartMonth { get; set; }

    public int? EndYear { get; set; }

    public int EndMonth { get; set; }

    public bool IsPresent { get; set; }

    public string? Grade { get; set; }

    public int? Percentage { get; set; }

    public string? Degree { get; set; }

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public int? DisplayOrder { get; set; }

    public virtual AspNetUser? CreatedByNavigation { get; set; }

    public virtual Profile Profile { get; set; } = null!;

    public virtual AspNetUser? UpdatedByNavigation { get; set; }
}
