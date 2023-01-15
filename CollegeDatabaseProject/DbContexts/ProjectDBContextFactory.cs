using Microsoft.EntityFrameworkCore;

namespace CollegeDatabaseProject.DbContexts;

public class ProjektDbContextFactory
{
    private string _connectionString = "";
    private string _mySqlServerVersion = "";

    public ProjektDbContextFactory(string connectionString, string mySqlServerVersion)
    {
        _connectionString = connectionString;
        _mySqlServerVersion = mySqlServerVersion;
    }

    public ProjektDbContext CreateDbContext()
    {
        DbContextOptions options = new DbContextOptionsBuilder().UseMySql
            (_connectionString,new MySqlServerVersion(_mySqlServerVersion)).Options;
        return new ProjektDbContext(options);
    }

}