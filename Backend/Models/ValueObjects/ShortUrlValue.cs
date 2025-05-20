namespace Backend.Models.ValueObjects;

public class ShortUrlValue(string value)  
{
    private const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
    private const int ShortCodeLength = 6;
    private readonly Random _random = new();
    private readonly string _value = value;

    public ShortUrlValue() : this(string.Empty)
    {
        _value = new string(Enumerable.Repeat(0, ShortCodeLength).Select((i) => GetRandomChar()).ToArray());
    }

    public override string ToString()
    {
        return _value;
    }

    public static implicit operator string(ShortUrlValue obj)
    {
        return obj._value;
    }

    public static implicit operator ShortUrlValue(string shortUrl)
    {
        return new ShortUrlValue(shortUrl);
    }

    private char GetRandomChar() => AllowedChars[_random.Next(AllowedChars.Length)];
}