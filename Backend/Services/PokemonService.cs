using Backend.HttpClients;
using Backend.Models.DTOs;
using Backend.Models.Entities;
using Backend.Repositories;

namespace Backend.Services
{
    public class PokemonService
    {
        private readonly PokeApiClient _pokeApiClient;
        private readonly PokemonRepository _repository;

        public PokemonService(
            PokeApiClient pokeApiClient,
            PokemonRepository repository)
        {
            _pokeApiClient = pokeApiClient;
            _repository = repository;
        }

        // Listado paginado
        public async Task<List<PokemonDto>> GetPokemonsAsync(int limit, int offset)
        {
            var response = await _pokeApiClient.GetPokemonsAsync(limit, offset);

            var pokemons = new List<PokemonDto>();

            foreach (var p in response.Results)
            {
                var id = ExtractIdFromUrl(p.Url);

                if (!await _repository.ExistsAsync(id))
                {
                    await _repository.AddAsync(new PokemonEntity
                    {
                        PokemonId = id,
                        Name = p.Name
                    });
                }

                pokemons.Add(new PokemonDto
                {
                    Id = id,
                    Name = p.Name
                });
            }

            return pokemons;
        }

        // Buscar por nombre
        public async Task<PokemonDto?> GetPokemonByNameAsync(string name)
        {
            var response = await _pokeApiClient.GetPokemonByNameAsync(name);
            if (response == null) return null;

            if (!await _repository.ExistsAsync(response.Id))
            {
                await _repository.AddAsync(new PokemonEntity
                {
                    PokemonId = response.Id,
                    Name = response.Name
                });
            }

            return new PokemonDto
            {
                Id = response.Id,
                Name = response.Name
            };
        }

        // Listado desde DB
        public async Task<List<PokemonEntity>> GetFromDbAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Extraer ID de la URL de PokeAPI
        private int ExtractIdFromUrl(string url)
        {
            var parts = url.TrimEnd('/').Split('/');
            return int.Parse(parts.Last());
        }
    }
}
