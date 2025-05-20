namespace Backend.Models.Exceptions;

public class OriginalUrlFormatException(string originalUrl) : Exception($"URL {originalUrl} is not in a valid format.")
{
}