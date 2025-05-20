using Backend.Models;
using Backend.Models.ValueObjects;

namespace Backend.Features.ShortenUrl;

public interface ICreateShortUrlUseCase
{
    public Task<Url> ExecuteAsync(OriginalUrlValue originalUrl);
}

public class CreateShortUrlUseCase(IShortenUrlService shortenUrlService) : ICreateShortUrlUseCase
{
    public async Task<Url> ExecuteAsync(OriginalUrlValue originalUrl)
    {
        return
            await shortenUrlService.FindByOriginalUrlAsync(originalUrl) ??
            await shortenUrlService.SaveNewShortUrl(new Url(originalUrl));
    }
}