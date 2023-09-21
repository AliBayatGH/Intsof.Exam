using Application.Common;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistance;
public class TestContext : DbContext, ITestContext
{
    public TestContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public async Task SaveAsync(CancellationToken ct)
    {
        await SaveChangesAsync(ct);
    }

    public DbSet<User> Users { get; set; }
}
