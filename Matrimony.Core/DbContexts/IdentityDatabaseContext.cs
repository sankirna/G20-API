using Matrimony.Core.IndentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Matrimony.Core.DbContexts;

public class IdentityDatabaseContext : IdentityDbContext<IdentityUser>
{
    public IdentityDatabaseContext()
    {
            
    }
    public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
        : base(options)
    {
    }

    protected IdentityDatabaseContext(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
        //modelBuilder.Ignore<IdentityUserRole<string>>();
        //modelBuilder.Ignore<IdentityUserClaim<string>>();
        //modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
        //modelBuilder.Ignore<IdentityUser<string>>();
        //modelBuilder.Ignore<ApplicationUser>();
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
