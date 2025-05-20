using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Frontend.Services;

public record ShortenUrlResultDto(
    string OriginalUrl,
    string ShortenUrl
);

public struct ShortenUrlResultData
{
    public string originalUrl { get; set; }
    public string shortenUrl { get; set; }
}

public interface IUrlShortenerService
{
    public Task<ShortenUrlResultDto> ShortenUrlAsync(string url);
}

public class UrlShortenerService(HttpClient client) : IUrlShortenerService
{
    public async Task<ShortenUrlResultDto> ShortenUrlAsync(string url)
    {
        var response = await client.PostAsync("/shorten-url", JsonContent.Create(new { url }));
        
        try
        {
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ShortenUrlResultData>();
            return new ShortenUrlResultDto(result.originalUrl, result.shortenUrl);
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == HttpStatusCode.InternalServerError)
                throw new Exception("An unexpected error ocurred.");
            
            var error = await response.Content.ReadFromJsonAsync<string>();
            throw new Exception(error);
        }
    }
}