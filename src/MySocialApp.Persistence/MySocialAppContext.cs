using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySocialApp.Domain;

namespace MySocialApp.Persistence;

public class MySocialAppContext : IdentityDbContext<User>
{
    public MySocialAppContext()
    {
        
    }

    public MySocialAppContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(MySocialAppContext).Assembly);

        base.OnModelCreating(builder);
    }
}
