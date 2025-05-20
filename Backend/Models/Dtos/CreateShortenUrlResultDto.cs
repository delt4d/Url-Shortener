namespace Backend.Models.Dtos;

public record CreateShortenUrlResultDto(
    string OriginalUrl,
    string ShortenUrl)
{
    public IResult ToResult()
    {
        return Results.Json(new
        {
            OriginalUrl,
            ShortenUrl = $"http://localhost:5103/s/" + ShortenUrl
        });
    }
}