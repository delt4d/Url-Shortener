using Backend.Features.ShortenUrl;
using Microsoft.EntityFrameworkCore;

namespace Backend.Configuration;

public static class AppServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        ConfigureServices(builder.Services, builder.Configuration);
    }

    private static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton<IEnvService, EnvService>();
        services.AddTransient<IShortenUrlService, ShortenUrlService>();
        services.AddTransient<ICreateShortUrlUseCase, CreateShortUrlUseCase>();
        services.AddTransient<IFindShortUrlUseCase, FindShortUrlUseCase>();
        
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddControllers();
        services.AddHostedService<MigrationService>();
    }
}