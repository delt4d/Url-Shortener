namespace Backend.Configuration;

public interface IEnvService
{
    public bool IsDev { get; }
    public bool IsProd { get; }
    public bool IsTest { get; }
}

public sealed class EnvService(IWebHostEnvironment env) : IEnvService
{
    public bool IsDev { get; } = env.IsDevelopment();
    public bool IsProd { get; } = env.IsProduction();
    public bool IsTest { get; } = false;
}