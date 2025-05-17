using Backend.Configuration;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.ShortenUrl;

public interface IShortenUrlService
{
    public Task<Url?> FindByOriginalUrlAsync(string originalUrl);

    public Task<Url?> FindByShortenedUrlAsync(string shortUrl);

    public Task<Url> SaveNewShortUrl(Url url);
}

public class ShortenUrlService(AppDbContext context) : IShortenUrlService
{
    public async Task<Url?> FindByOriginalUrlAsync(string originalUrl)
    {
        var result = await context.Urls.SingleOrDefaultAsync(x => x.OriginalUrl == originalUrl);
        return result is not null ? new Url(result) : null;
    }

    public async Task<Url?> FindByShortenedUrlAsync(string shortUrl)
    {
        var result = await context.Urls.SingleOrDefaultAsync(x => x.ShortUrl == shortUrl);
        return result is not null ? new Url(result) : null;
    }

    public async Task<Url> SaveNewShortUrl(Url url)
    {
        var table = new UrlEntity
        {
            Id = url.Id,
            OriginalUrl = url.OriginalUrl,
            ShortUrl = url.ShortUrl.Value
        };
        var result = await context.Urls.AddAsync(table);

        await context.SaveChangesAsync();

        return new Url(result.Entity);
    }
}