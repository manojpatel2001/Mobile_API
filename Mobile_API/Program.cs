
using Microsoft.EntityFrameworkCore;
using Mobile_Core.DB;
using Mobile_Infrastructure.Interface;
using Mobile_Infrastructure.Repository;
using Serilog;

namespace Mobile_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Serilog early
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Information)
                .WriteTo.Console()
                .WriteTo.File("logs/application-.txt",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30, // Keep 30 days of logs
                    fileSizeLimitBytes: 10_000_000, // 10MB per file
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {SourceContext}: {Message:lj} {Properties:j}{NewLine}{Exception}")
                .CreateLogger();

            try
            {
                Log.Information("Starting web application");

                var builder = WebApplication.CreateBuilder(args);

                // Use Serilog
                builder.Host.UseSerilog();

                builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
                builder.Services.AddDbContext<MobileDbcontext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("MobileConnection"));
                });

                // Add services to the container.
                builder.Services.AddControllers();
                builder.Services.AddHttpClient();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                // With this:
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mobile API V1");
                    c.RoutePrefix = "swagger"; // Optional: sets Swagger UI at /swagger
                });

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();

                Log.Information("Web application configured successfully");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
