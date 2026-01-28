namespace Models.External
{
    public class PokeApiListResponse
    {
        public int Count { get; set; }
        public List<PokeApiResult> Results { get; set; } = new();
    }
}
