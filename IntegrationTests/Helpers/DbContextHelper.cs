using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data;

namespace IntegrationTests.Helpers
{
    public class DbContextHelper
    {
        public AppDbContext Context { get; set; }
        public DbContextHelper()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("UNIT_TESTING")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            var options = builder.Options;
            Context = new AppDbContext(options);
        }
    }
}
