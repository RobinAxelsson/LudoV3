using LudoEngine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LudoEngine.DbModel
{
    public class LudoContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Pawn> Pawns { get; set; }

        public LudoContext() : base()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            DatabaseManagement.ReadConnectionString(@"DbModel/connection.txt");
            optionsbuilder.UseSqlServer(DatabaseManagement.ConnectionString);
        }
    }
}