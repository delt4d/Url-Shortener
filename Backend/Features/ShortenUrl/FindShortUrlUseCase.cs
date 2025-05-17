using Backend.Models;

namespace Backend.Features.ShortenUrl;

public interface IFindShortUrlUseCase
{
    public Task<Url> ExecuteAsync(string originalUrl);
}

public class FindShortUrlUseCase(IShortenUrlService shortenUrlService) : IFindShortUrlUseCase
{
    public async Task<Url> ExecuteAsync(string shortUrl)
    {
        var url = await shortenUrlService.FindByShortenedUrlAsync(shortUrl);
        return url ?? throw new ShortUrlNotFound(shortUrl);
    }
}