using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CollegeDatabaseProject.DbContexts;

public class ProjektDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProjektDbContext>
{
    public ProjektDbContext CreateDbContext(string[] args)
    {
        DbContextOptions options = new DbContextOptionsBuilder().UseMySql
            (new MySqlServerVersion("8.0.30")).Options;
        return new ProjektDbContext(options);
    }
}