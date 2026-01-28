namespace Backend.Models.Entities
{
    public class PokemonEntity
    {
        public int Id { get; set; }          // ID interno (PK)
        public int PokemonId { get; set; }   // ID real de la PokeAPI
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
