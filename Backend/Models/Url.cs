namespace Backend.Models;

public class Url
{
    public Guid Id { get; }
    public string OriginalUrl { get; set; }
    public ShortUrl ShortUrl { get; set; }

    public Url(string originalUrl)
    {
        Id = Guid.NewGuid();
        OriginalUrl = originalUrl;
        ShortUrl = new ShortUrl();
    }

    public Url(UrlEntity entity)
    {
        Id = entity.Id;
        OriginalUrl = entity.OriginalUrl;
        ShortUrl = new ShortUrl(entity.ShortUrl);
    }
}