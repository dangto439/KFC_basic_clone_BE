using KFC.API.DI;
using KFC.API.Middleware;
using KFC.Core.Utils;
using KFC.Services;

var builder = WebApplication.CreateBuilder(args);
// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);


builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();


builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
var environment = app.Environment;

Console.WriteLine($"Current Environment: {environment.EnvironmentName}");
app.UseStaticFiles();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseStaticFiles();
app.MapControllers();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

//setting Middleware
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<PermissionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();


//app.MapHub<SignalRService>("/ws").RequireCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
Console.WriteLine($"Time Start:{CoreHelper.SystemTimeNow}");
app.Run();