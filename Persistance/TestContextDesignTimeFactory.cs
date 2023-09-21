using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistance;

public class TestContextDesignTimeFactory : IDesignTimeDbContextFactory<TestContext>
{
    public TestContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TestContext>();
        optionsBuilder.UseSqlServer("Server=.;Database=Test;UId=sa;Password=123QWeewb;Encrypt=False;");

        return new TestContext(optionsBuilder.Options);
    }
}
