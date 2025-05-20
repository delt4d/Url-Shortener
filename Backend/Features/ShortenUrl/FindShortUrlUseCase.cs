using Backend.Models;
using Backend.Models.Exceptions;
using Backend.Models.ValueObjects;

namespace Backend.Features.ShortenUrl;

public interface IFindShortUrlUseCase
{
    public Task<Url> ExecuteAsync(ShortUrlValue shortUrl);
}

public class FindShortUrlUseCase(IShortenUrlService shortenUrlService) : IFindShortUrlUseCase
{
    public async Task<Url> ExecuteAsync(ShortUrlValue shortUrl)
    {
        var url = await shortenUrlService.FindByShortenedUrlAsync(shortUrl);
        return url ?? throw new ShortUrlNotFoundException(shortUrl);
    }
}