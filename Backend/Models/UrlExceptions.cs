namespace Backend.Models;

public class ShortUrlNotFound(string shortUrl) : Exception($"Short url {shortUrl} not found.")
{
}