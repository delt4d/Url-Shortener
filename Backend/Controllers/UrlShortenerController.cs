using Backend.Configuration;
using Backend.Features.ShortenUrl;
using Backend.Models;
using Backend.Models.Dtos;
using Backend.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
public class UrlShortenerController(
    ICreateShortUrlUseCase createShortUrl,
    IFindShortUrlUseCase findShortUrl,
    IEnvService envService) : ControllerBase
{
    [HttpGet("/healthcheck")]
    public IActionResult HealthCheck() => Ok();

    [HttpPost("/shorten-url")]
    public async Task<IResult> CreateShortenUrl(CreateShortenUrlDto data)
    {
        try
        {
            var result = await createShortUrl.ExecuteAsync(data.Url);
            var dto = new CreateShortenUrlResultDto(result.OriginalUrl, result.ShortUrl);
            return dto.ToResult();  
        }
        catch (OriginalUrlFormatException ex)
        {
            return Results.BadRequest(ex.Message);
        }
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
        catch (ShortUrlNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}