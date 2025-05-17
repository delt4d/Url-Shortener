namespace Backend.Models;

public class ShortUrl
{
    private const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
    private const int ShortCodeLength = 6;
    private readonly Random _random = new();
    
    public string Value { get; }

    public ShortUrl()
    {
        Value = new string(Enumerable.Repeat(0, ShortCodeLength).Select((i) => GetRandomChar()).ToArray());
    }

    public ShortUrl(string value)
    {
        Value = value;
    }

    private char GetRandomChar() => AllowedChars[_random.Next(AllowedChars.Length)];
}