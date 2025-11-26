using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace XkcdComicFinder;

public record Comic
{
    [Key]
    [JsonPropertyName("num")]
    public int Number { get; init; }
    [JsonPropertyName("safe_title")]
    public string? Title { get; init; }
    [JsonPropertyName("month")]
    public string? Month { get; init; }
    [JsonPropertyName("day")]
    public string? Day { get; init; }
    [JsonPropertyName("year")]
    public string? Year { get; init; }
    [JsonIgnore]
    public DateOnly Date => DateOnly.Parse($"{Year}-{Month}-{Day}");

    
}
