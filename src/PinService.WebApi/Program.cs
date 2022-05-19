using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PinService.Application;
using PinService.Cache;
using PinService.Infrastructure;

namespace PinService.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services
        .AddApplicationServices()
        .AddPersistance(builder.Configuration)
        .AddCacheServices(builder.Configuration);

        builder.Services.ConfigureHealthChecks(builder.Configuration);

        builder.Services.AddAuthentication();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(
            endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("health-ui", new HealthCheckOptions
                {
                    AllowCachingResponses = false,
                    ResultStatusCodes =
                    {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    },

                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecksUI();
            });
        app.Run();
    }
}

