using LoupGarou.Model;
using Microsoft.EntityFrameworkCore;

namespace LoupGarou.Data
{
    public class LoupGarouDbContext : DbContext
    {
        public LoupGarouDbContext(DbContextOptions<LoupGarouDbContext> options)
            : base(options) 
        {}

        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
