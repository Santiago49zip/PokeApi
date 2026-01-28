using Backend.Data;
using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class PokemonRepository
    {
        private readonly ApplicationDbContext _context;

        public PokemonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int pokemonId)
        {
            return await _context.Pokemons.AnyAsync(p => p.PokemonId == pokemonId);
        }

        public async Task AddAsync(PokemonEntity pokemon)
        {
            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PokemonEntity>> GetAllAsync()
        {
            return await _context.Pokemons.ToListAsync();
        }
    }
}
