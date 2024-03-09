using LoupGarou.Model;
using Microsoft.EntityFrameworkCore;
using Action = LoupGarou.Model.Action;

namespace LoupGarou.Data
{
    public class LoupGarouDbContext : DbContext
    {
        public LoupGarouDbContext(DbContextOptions<LoupGarouDbContext> options)
            : base(options) 
        {}

        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Action> Actions{ get; set; }
    }
}
