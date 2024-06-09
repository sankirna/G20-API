using System;
using System.Collections.Generic;
using G20.Core.Domain;
using G20.Core.IndentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace G20.Core.DbContexts;

public partial class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
: base(options)
    {
    }
}
