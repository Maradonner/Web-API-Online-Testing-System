using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TestingSystem.Data;

namespace IntegrationTests.Helpers;

public class DbContextHelper
{
    public DbContextHelper()
    {
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseInMemoryDatabase("UNIT_TESTING")
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

        var options = builder.Options;
        Context = new AppDbContext(options);
    }

    public AppDbContext Context { get; set; }
}