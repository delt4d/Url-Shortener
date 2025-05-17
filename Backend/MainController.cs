using Backend.Configuration;
using Backend.Features.ShortenUrl;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend;

[ApiController]
public class MainController(
    ICreateShortUrlUseCase createShortUrl,
    IFindShortUrlUseCase findShortUrl,
    IEnvService envService) : ControllerBase
{
    [HttpGet("/healthcheck")]
    public IActionResult HealthCheck() => Ok();

    [HttpPost("/shorten-url")]
    public async Task<IResult> CreateShortenUrl(CreateShortenUrlDto data)
    {
        var result = await createShortUrl.ExecuteAsync(data.Url);
        return Results.Json(result);
    }
    
    [HttpGet("/s/{shortUrl}")]
    public async Task<IActionResult> RedirectFromShortUrl(string shortUrl)
    {
        try
        {
            var result = await findShortUrl.ExecuteAsync(shortUrl);
            var originalUrl = result.OriginalUrl;
            return envService.IsDev ? Redirect(originalUrl) : RedirectPermanent(originalUrl);
        }
        catch (ShortUrlNotFound ex)
        {
            return NotFound(ex.Message);
        }
    }
}