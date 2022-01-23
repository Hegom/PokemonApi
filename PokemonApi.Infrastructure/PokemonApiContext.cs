using Microsoft.EntityFrameworkCore;
using PokemonApi.Core.Model;

namespace PokemonApi.Infrastructure
{
    public class PokemonApiContext : DbContext
    {
        public PokemonApiContext(DbContextOptions<PokemonApiContext> options)
            : base(options)
        {
        }        

        public DbSet<User> User { get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PokemonApiContext).Assembly);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Pokemon>().HasKey(x => x.Id);
        }
    }
}
