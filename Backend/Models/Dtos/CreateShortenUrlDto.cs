using System.Text.Json.Serialization;

namespace Backend.Models.Dtos;

public record CreateShortenUrlDto(
    [property: JsonPropertyName("url")]
    string Url
);
