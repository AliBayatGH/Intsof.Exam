using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Intsoft.Exam.IntegrationTests;
public class TestContextFixture : IDisposable
{
    public TestContext Context { get; private set; }

    public TestContextFixture()
    {
        var options = new DbContextOptionsBuilder<TestContext>()
            .UseSqlServer("Server=.;Database=Test;UId=sa;Password=123QWeewb;Encrypt=False;")
            .Options;

        Context = new TestContext(options);
    }
    public void Dispose()
    {
        Context.Dispose();
    }
}

[CollectionDefinition("Context Collection")]
public class ContextCollection : ICollectionFixture<TestContextFixture> { }
