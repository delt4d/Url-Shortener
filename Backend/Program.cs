using Backend.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("development");
app.MapControllers();
app.Run();
