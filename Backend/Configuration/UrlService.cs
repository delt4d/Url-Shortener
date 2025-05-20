using Backend.Models.Exceptions;

namespace Backend.Configuration;

public static class UrlService
{
    public static string NormalizeUrl(string url)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(url))
                return string.Empty;

            var uri = new Uri(url);
            var scheme = uri.Scheme.ToLowerInvariant();
            var host = uri.Host.ToLowerInvariant();
            var port = uri.IsDefaultPort ? string.Empty : $":{uri.Port}";
            var path = string.IsNullOrEmpty(uri.AbsolutePath) || uri.AbsolutePath == "/"
                ? string.Empty
                : uri.AbsolutePath.TrimEnd('/');
            var query = string.IsNullOrEmpty(uri.Query) || uri.Query == "?" ? string.Empty : uri.Query;

            return $"{scheme}://{host}{port}{path}{query}";
        }
        catch (UriFormatException)
        {
            throw new OriginalUrlFormatException(url);
        }
    }
}
