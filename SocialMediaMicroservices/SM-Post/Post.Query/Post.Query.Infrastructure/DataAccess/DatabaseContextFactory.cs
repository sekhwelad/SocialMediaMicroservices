using Microsoft.EntityFrameworkCore;

namespace Post.Query.Infrastructure.DataAccess
{
    public class DatabaseContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configurationDbContext;

        public DatabaseContextFactory(Action<DbContextOptionsBuilder> configurationDbContext)
        {
            _configurationDbContext = configurationDbContext ?? throw new ArgumentNullException(nameof(configurationDbContext));
        }

        public DatabaseContext CreateDbContext()
        {
            DbContextOptionsBuilder<DatabaseContext> optionsBuilder = new();
            _configurationDbContext(optionsBuilder);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
