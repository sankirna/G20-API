using System;
using System.Collections.Generic;
using Matrimony.Core.Domain;
using Matrimony.Core.IndentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Matrimony.Core.DbContexts;

public partial class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
: base(options)
    {
    }
}
