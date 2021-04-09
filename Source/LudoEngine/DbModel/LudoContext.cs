using LudoEngine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LudoEngine.DbModel
{
    public class LudoContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<PawnSavePoint> PawnSavePoints { get; set; }
        public DbSet<PlayerGame> GamePlayers { get; set; }
        public LudoContext() : base()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            DatabaseManagement.ReadConnectionString(@"DbModel/connection.txt");
            optionsbuilder.UseSqlServer(DatabaseManagement.ConnectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerGame>()
                .HasKey(x => new { x.GameId, x.PlayerId });

            modelBuilder.Entity<Game>()
                .HasKey(x => x.Id);
        }
    }
}