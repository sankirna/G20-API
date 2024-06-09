//using System;
//using System.Collections.Generic;
//using Matrimony.Core.Domain;
//using Microsoft.EntityFrameworkCore;

//namespace Matrimony.Core.Domain;

//public partial class MatrimonyContext : DbContext
//{
//    public MatrimonyContext()
//    {
//    }

//    public MatrimonyContext(DbContextOptions<MatrimonyContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Achivement> Achivements { get; set; }

//    public virtual DbSet<Address> Addresses { get; set; }

//    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

//    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

//    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

//    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

//    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

//    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

//    public virtual DbSet<City> Cities { get; set; }

//    public virtual DbSet<Country> Countries { get; set; }

//    public virtual DbSet<Education> Educations { get; set; }

//    public virtual DbSet<Family> Families { get; set; }

//    public virtual DbSet<File> Files { get; set; }

//    public virtual DbSet<MigrationVersionInfo> MigrationVersionInfos { get; set; }

//    public virtual DbSet<Occupation> Occupations { get; set; }

//    public virtual DbSet<Profile> Profiles { get; set; }

//    public virtual DbSet<State> States { get; set; }

//    public virtual DbSet<Test> Tests { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-O2RGDVD;Initial Catalog=matrimony;Integrated Security=True;Encrypt=False");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Achivement>(entity =>
//        {
//            entity.ToTable("Achivement");

//            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
//            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

//            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AchivementCreatedByNavigations)
//                .HasForeignKey(d => d.CreatedBy)
//                .HasConstraintName("FK_Achivement_AspNetUsers");

//            entity.HasOne(d => d.Profile).WithMany(p => p.Achivements)
//                .HasForeignKey(d => d.ProfileId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Achivement_Profile");

//            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.AchivementUpdatedByNavigations)
//                .HasForeignKey(d => d.UpdatedBy)
//                .HasConstraintName("FK_Achivement_AspNetUsers1");
//        });

//        modelBuilder.Entity<Address>(entity =>
//        {
//            entity.ToTable("Address");

//            entity.Property(e => e.Address1)
//                .HasMaxLength(10)
//                .IsFixedLength();
//            entity.Property(e => e.Address2)
//                .HasMaxLength(10)
//                .IsFixedLength();
//            entity.Property(e => e.CityId)
//                .HasMaxLength(10)
//                .IsFixedLength();
//            entity.Property(e => e.CountryId)
//                .HasMaxLength(10)
//                .IsFixedLength();
//            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
//            entity.Property(e => e.Landmark)
//                .HasMaxLength(10)
//                .IsFixedLength();
//            entity.Property(e => e.PinNo)
//                .HasMaxLength(10)
//                .IsFixedLength();
//            entity.Property(e => e.StateId)
//                .HasMaxLength(10)
//                .IsFixedLength();
//            entity.Property(e => e.TypeId)
//                .HasMaxLength(10)
//                .IsFixedLength();
//            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

//            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AddressCreatedByNavigations)
//                .HasForeignKey(d => d.CreatedBy)
//                .HasConstraintName("FK_Address_AspNetUsers");

//            entity.HasOne(d => d.Profile).WithMany(p => p.Addresses)
//                .HasForeignKey(d => d.ProfileId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Address_Profile");

//            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.AddressUpdatedByNavigations)
//                .HasForeignKey(d => d.UpdatedBy)
//                .HasConstraintName("FK_Address_AspNetUsers1");
//        });

//        modelBuilder.Entity<AspNetRole>(entity =>
//        {
//            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
//                .IsUnique()
//                .HasFilter("([NormalizedName] IS NOT NULL)");

//            entity.Property(e => e.Name).HasMaxLength(256);
//            entity.Property(e => e.NormalizedName).HasMaxLength(256);
//        });

//        modelBuilder.Entity<AspNetRoleClaim>(entity =>
//        {
//            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

//            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
//        });

//        modelBuilder.Entity<AspNetUser>(entity =>
//        {
//            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

//            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
//                .IsUnique()
//                .HasFilter("([NormalizedUserName] IS NOT NULL)");

//            entity.Property(e => e.Email).HasMaxLength(256);
//            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
//            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
//            entity.Property(e => e.UserName).HasMaxLength(256);

//            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
//                .UsingEntity<Dictionary<string, object>>(
//                    "AspNetUserRole",
//                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
//                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
//                    j =>
//                    {
//                        j.HasKey("UserId", "RoleId");
//                        j.ToTable("AspNetUserRoles");
//                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
//                    });
//        });

//        modelBuilder.Entity<AspNetUserClaim>(entity =>
//        {
//            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

//            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
//        });

//        modelBuilder.Entity<AspNetUserLogin>(entity =>
//        {
//            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

//            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

//            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
//        });

//        modelBuilder.Entity<AspNetUserToken>(entity =>
//        {
//            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

//            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
//        });

//        modelBuilder.Entity<City>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__City__3214EC0713C8B88C");

//            entity.ToTable("City");

//            entity.Property(e => e.Name)
//                .HasMaxLength(100)
//                .IsUnicode(false);

//            entity.HasOne(d => d.State).WithMany(p => p.Cities)
//                .HasForeignKey(d => d.StateId)
//                .HasConstraintName("FK__City__StateId__06CD04F7");
//        });

//        modelBuilder.Entity<Country>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC0783445C3A");

//            entity.ToTable("Country");

//            entity.Property(e => e.Name)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Education>(entity =>
//        {
//            entity.ToTable("Education");

//            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
//            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

//            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.EducationCreatedByNavigations)
//                .HasForeignKey(d => d.CreatedBy)
//                .HasConstraintName("FK_Education_AspNetUsers");

//            entity.HasOne(d => d.Profile).WithMany(p => p.Educations)
//                .HasForeignKey(d => d.ProfileId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Education_Profile");

//            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.EducationUpdatedByNavigations)
//                .HasForeignKey(d => d.UpdatedBy)
//                .HasConstraintName("FK_Education_AspNetUsers1");
//        });

//        modelBuilder.Entity<Family>(entity =>
//        {
//            entity.ToTable("Family");

//            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
//            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

//            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.FamilyCreatedByNavigations)
//                .HasForeignKey(d => d.CreatedBy)
//                .HasConstraintName("FK_Family_AspNetUsers");

//            entity.HasOne(d => d.Profile).WithMany(p => p.Families)
//                .HasForeignKey(d => d.ProfileId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Family_Profile");

//            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.FamilyUpdatedByNavigations)
//                .HasForeignKey(d => d.UpdatedBy)
//                .HasConstraintName("FK_Family_AspNetUsers1");
//        });

//        modelBuilder.Entity<File>(entity =>
//        {
//            entity.ToTable("File");

//            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
//            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

//            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.FileCreatedByNavigations)
//                .HasForeignKey(d => d.CreatedBy)
//                .HasConstraintName("FK_File_AspNetUsers");

//            entity.HasOne(d => d.Profile).WithMany(p => p.Files)
//                .HasForeignKey(d => d.ProfileId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_File_Profile");

//            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.FileUpdatedByNavigations)
//                .HasForeignKey(d => d.UpdatedBy)
//                .HasConstraintName("FK_File_AspNetUsers1");
//        });

//        modelBuilder.Entity<MigrationVersionInfo>(entity =>
//        {
//            entity
//                .HasNoKey()
//                .ToTable("MigrationVersionInfo");

//            entity.HasIndex(e => e.Version, "UC_Version")
//                .IsUnique()
//                .IsClustered();

//            entity.Property(e => e.AppliedOn).HasColumnType("datetime");
//            entity.Property(e => e.Description).HasMaxLength(1024);
//        });

//        modelBuilder.Entity<Occupation>(entity =>
//        {
//            entity.ToTable("Occupation");

//            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
//            entity.Property(e => e.EndDate).HasColumnType("datetime");
//            entity.Property(e => e.StartDate).HasColumnType("datetime");
//            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
//        });

//        modelBuilder.Entity<Profile>(entity =>
//        {
//            entity.ToTable("Profile");

//            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
//            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

//            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProfileCreatedByNavigations)
//                .HasForeignKey(d => d.CreatedBy)
//                .HasConstraintName("FK_Profile_AspNetUsers1");

//            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ProfileUpdatedByNavigations)
//                .HasForeignKey(d => d.UpdatedBy)
//                .HasConstraintName("FK_Profile_AspNetUsers2");

//            entity.HasOne(d => d.User).WithMany(p => p.ProfileUsers)
//                .HasForeignKey(d => d.UserId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Profile_AspNetUsers");
//        });

//        modelBuilder.Entity<State>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__State__3214EC072197E05D");

//            entity.ToTable("State");

//            entity.Property(e => e.CountryId).HasColumnName("CountryID");
//            entity.Property(e => e.Name)
//                .HasMaxLength(100)
//                .IsUnicode(false);

//            entity.HasOne(d => d.Country).WithMany(p => p.States)
//                .HasForeignKey(d => d.CountryId)
//                .HasConstraintName("FK__State__CountryID__07C12930");
//        });

//        modelBuilder.Entity<Test>(entity =>
//        {
//            entity.ToTable("Test");

//            entity.Property(e => e.Name)
//                .HasMaxLength(10)
//                .IsFixedLength();
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
