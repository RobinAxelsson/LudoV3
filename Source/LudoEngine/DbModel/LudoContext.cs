using LudoEngine.Models;
using Microsoft.EntityFrameworkCore;

namespace LudoEngine.DbModel
{
    public class StarWarsContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Pawn> Pawns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            DatabaseManagement.ReadConnectionString(@"DbModel/connection.txt");
            optionsbuilder.UseSqlServer(DatabaseManagement.ConnectionString);
        }
    }
}