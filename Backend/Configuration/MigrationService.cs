using Microsoft.EntityFrameworkCore;

namespace Backend.Configuration;

public class MigrationServiceOptions
{
    public int Retries { get; set; }
    public int Interval { get; set; }
}

public sealed class MigrationService(IServiceProvider services, IEnvService envService) : BackgroundService 
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var options = new MigrationServiceOptions
        {
            Retries = 10,
            Interval = 3000
        };
        
        if (!envService.IsDev) return;

        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        for (var i = 0; i < options.Retries; i++)
        {
            if (cancellationToken.IsCancellationRequested) 
                break;
            
            try
            {
                await context.Database.MigrateAsync(cancellationToken);
                break;
            }
            catch
            {
                await Task.Delay(options.Interval, cancellationToken);
            }
        }
    }
}