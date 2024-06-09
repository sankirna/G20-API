using Matrimony.Core.Domain;
using System;
using System.Collections.Generic;

namespace Matrimony.Core.Domain;

public partial class File : BaseEntity
{
    public string? Name { get; set; }
    public string? OriginalName { get; set; }

    public int TypeId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual AspNetUser? CreatedByNavigation { get; set; }

    public virtual ICollection<ProfileFile> ProfileFiles { get; set; } = new List<ProfileFile>();

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();

    public virtual AspNetUser? UpdatedByNavigation { get; set; }
}
