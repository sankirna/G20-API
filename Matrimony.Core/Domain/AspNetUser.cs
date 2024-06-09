using Matrimony.Core.Domain;
using System;
using System.Collections.Generic;

namespace Matrimony.Core.Domain;

public partial class AspNetUser
{
    public int Id { get; set; }

    public int Role { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public virtual ICollection<Achivement> AchivementCreatedByNavigations { get; set; } = new List<Achivement>();

    public virtual ICollection<Achivement> AchivementUpdatedByNavigations { get; set; } = new List<Achivement>();

    public virtual ICollection<Address> AddressCreatedByNavigations { get; set; } = new List<Address>();

    public virtual ICollection<Address> AddressUpdatedByNavigations { get; set; } = new List<Address>();

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<Education> EducationCreatedByNavigations { get; set; } = new List<Education>();

    public virtual ICollection<Education> EducationUpdatedByNavigations { get; set; } = new List<Education>();

    public virtual ICollection<Family> FamilyCreatedByNavigations { get; set; } = new List<Family>();

    public virtual ICollection<Family> FamilyUpdatedByNavigations { get; set; } = new List<Family>();

    public virtual ICollection<File> FileCreatedByNavigations { get; set; } = new List<File>();

    public virtual ICollection<File> FileUpdatedByNavigations { get; set; } = new List<File>();

    public virtual ICollection<Profile> ProfileCreatedByNavigations { get; set; } = new List<Profile>();

    public virtual ICollection<Profile> ProfileUpdatedByNavigations { get; set; } = new List<Profile>();

    public virtual ICollection<Profile> ProfileUsers { get; set; } = new List<Profile>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
