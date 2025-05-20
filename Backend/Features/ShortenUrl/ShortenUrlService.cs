using Backend.Configuration;
using Backend.Models;
using Backend.Models.Entities;
using Backend.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.ShortenUrl;

public interface IShortenUrlService
{
    public Task<Url?> FindByOriginalUrlAsync(OriginalUrlValue originalUrl);

    public Task<Url?> FindByShortenedUrlAsync(ShortUrlValue shortUrl);

    public Task<Url> SaveNewShortUrl(Url url);
}

public class ShortenUrlService(AppDbContext context) : IShortenUrlService
{
    public async Task<Url?> FindByOriginalUrlAsync(OriginalUrlValue originalUrl)
    {
        var result = await context.Urls.SingleOrDefaultAsync(x => x.OriginalUrl == originalUrl);
        return result is not null ? new Url(result) : null;
    }

    public async Task<Url?> FindByShortenedUrlAsync(ShortUrlValue shortUrl)
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
            ShortUrl = url.ShortUrl
        };
        var result = await context.Urls.AddAsync(table);

        await context.SaveChangesAsync();

        return new Url(result.Entity);
    }
}