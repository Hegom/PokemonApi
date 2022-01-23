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
            modelBuilder.Entity<Pokemon>().HasData(
               new Pokemon { Id = 1, Name = "Zapdos", Evolution = "None", Weight = 100, Width = 100, CreatedyByUserId = 90, IsPublic = true },
               new Pokemon { Id = 2, Name = "Gyarados", Evolution = "None", Weight = 100, Width = 100, CreatedyByUserId = 95, IsPublic = true },
               new Pokemon { Id = 3, Name = "Bulbasaur", Evolution = "Ivysaur", Weight = 100, Width = 100, CreatedyByUserId = 99, IsPublic = true },
               new Pokemon { Id = 4, Name = "Eevee", Evolution = "Vaporeon, Jolteon, Flareon", Weight = 100, Width = 100, CreatedyByUserId = 99, IsPublic = true }
            );
        }
    }
}
