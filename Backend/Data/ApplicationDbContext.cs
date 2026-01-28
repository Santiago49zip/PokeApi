using Microsoft.EntityFrameworkCore;
using Backend.Models.Entities;

namespace Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<PokemonEntity> Pokemons => Set<PokemonEntity>();
    }
}
