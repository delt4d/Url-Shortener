using System.Text.Json.Serialization;

namespace Backend.Models;

public class CreateShortenUrlDto
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}