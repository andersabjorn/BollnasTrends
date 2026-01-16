using System.Text.Json.Serialization;

namespace BollnasTrends.Infrastructure.Dtos;

// Detta är klassen som Repositoryt skriker efter.
// Den måste vara "public" för att Repositoryt ska se den.

public class ScbResponse
{
    [JsonPropertyName("data")]
    public List<ScbDataRow> Data { get; set; } = new();
}

public class ScbDataRow
{
    [JsonPropertyName("key")]
    public List<string> Key { get; set; } = new();

    [JsonPropertyName("values")]
    public List<string> Values { get; set; } = new();
}