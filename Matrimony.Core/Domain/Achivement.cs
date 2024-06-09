using System;
using System.Collections.Generic;

namespace Matrimony.Core.Domain;

public partial class Achivement : BaseEntity
{
    public int ProfileId { get; set; }

    public string Name { get; set; } = null!;

    public int Year { get; set; }

    public string Description { get; set; } = null!;

    public int? DisplayOrder { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual AspNetUser? CreatedByNavigation { get; set; }

    public virtual Profile Profile { get; set; } = null!;

    public virtual AspNetUser? UpdatedByNavigation { get; set; }
}
