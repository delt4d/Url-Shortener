namespace Backend.Models.Exceptions;

public class ShortUrlNotFoundException(string shortUrl) : Exception($"Now shorten URL {shortUrl} was found.")
{
}