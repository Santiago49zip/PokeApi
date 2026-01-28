using System.Net.Http;
using System.Net.Http.Json;
using Models.External;

namespace Backend.HttpClients
{
    public class PokeApiClient
    {
        private readonly HttpClient _httpClient;

        public PokeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Listado paginado
        public async Task<PokeApiListResponse> GetPokemonsAsync(int limit, int offset)
        {
            return await _httpClient
                .GetFromJsonAsync<PokeApiListResponse>($"?limit={limit}&offset={offset}")
                ?? new PokeApiListResponse();
        }

        // Buscar por nombre
        public async Task<PokeApiPokemonResponse?> GetPokemonByNameAsync(string name)
        {
            try
            {
                return await _httpClient
                    .GetFromJsonAsync<PokeApiPokemonResponse>($"{name.ToLower()}");
            }
            catch
            {
                // Si no existe devuelve null
                return null;
            }
        }
    }
}
