using Backend.Configuration;

namespace Backend.Models.ValueObjects;

public class OriginalUrlValue()
{
    private string _value = string.Empty;
    private string Value
    {
        get => _value; 
        set => _value = UrlService.NormalizeUrl(value);
    }
    
    private OriginalUrlValue(string originalUrl) : this()
    {
        Value = originalUrl;
    }
    
    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(OriginalUrlValue obj)
    {
        return obj.Value;
    }

    public static implicit operator OriginalUrlValue(string originalUrl)
    {
        return new OriginalUrlValue(originalUrl);
    }
}