
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mobile_API.Services;
using Mobile_Core.DB;
using Mobile_Infrastructure.Interface;
using Mobile_Infrastructure.Repository;
using Serilog;
using System.Text;

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



                // Configure JWT Authentication
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

                // Configure Swagger
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                    // Add JWT support in Swagger
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                Array.Empty<string>()
                            }
                        });
                    });


              

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddHttpClient();
                builder.Services.AddHttpContextAccessor(); // ✅ Register IHttpContextAccessor
                builder.Services.AddScoped<FileUploadService>();



                var app = builder.Build();

                // Configure the HTTP request pipeline.
                // With this:
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mobile API V1");
                    c.RoutePrefix = "swagger"; // Optional: sets Swagger UI at /swagger
                });

                app.UseStaticFiles();
                app.UseHttpsRedirection();
                app.UseAuthentication(); // Enable authentication
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
