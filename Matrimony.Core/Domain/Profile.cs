using Matrimony.Core.Domain;
using System;
using System.Collections.Generic;

namespace Matrimony.Core.Domain;

public partial class Profile : BaseEntity
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public int Sex { get; set; }

    public string Email { get; set; } = null!;

    public string? AlternateEmail { get; set; }

    public string? PhoneNo { get; set; }

    public string? AlternatePhoneNo { get; set; }

    public string? Langauge { get; set; }

    public string? OtherInformation { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public int? ResumeFileId { get; set; }

    public virtual ICollection<Achivement> Achivements { get; set; } = new List<Achivement>();

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual AspNetUser? CreatedByNavigation { get; set; }

    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();

    public virtual ICollection<Family> Families { get; set; } = new List<Family>();

    public virtual File? ResumeFile { get; set; }

    public virtual AspNetUser? UpdatedByNavigation { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}
