using Microsoft.EntityFrameworkCore;

namespace CollegeDatabaseProject.DbContexts;

public class ProjektDbContext : Microsoft.EntityFrameworkCore.DbContext

{

    public ProjektDbContext(DbContextOptions options) : base(options)

    {
        

    }




    // public DbSet<LoginDTO> Login { get; set; }

    //  public DbSet<ReservationDTO> Reservations { get; set; }

}