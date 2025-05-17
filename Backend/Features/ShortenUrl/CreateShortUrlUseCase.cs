using Backend.Models;

namespace Backend.Features.ShortenUrl;

public interface ICreateShortUrlUseCase
{
    public Task<Url> ExecuteAsync(string originalUrl);
}

public class CreateShortUrlUseCase(IShortenUrlService shortenUrlService) : ICreateShortUrlUseCase
{
    public async Task<Url> ExecuteAsync(string originalUrl)
    {
        return
            await shortenUrlService.FindByOriginalUrlAsync(originalUrl) ??
            await shortenUrlService.SaveNewShortUrl(new Url(originalUrl));
    }
}