using Backend.Configuration;
using Backend.Models.Entities;
using Backend.Models.ValueObjects;

namespace Backend.Models;

public class Url
{
    public Guid Id { get; }
    public OriginalUrlValue OriginalUrl { get; set; }
    public ShortUrlValue ShortUrl { get; set; }

    public Url(OriginalUrlValue originalUrl)
    {
        Id = Guid.NewGuid();
        OriginalUrl = originalUrl;
        ShortUrl = new ShortUrlValue();
    }

    public Url(UrlEntity entity)
    {
        Id = entity.Id;
        OriginalUrl = entity.OriginalUrl;
        ShortUrl = entity.ShortUrl;
    }
}