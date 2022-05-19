
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PinService.Application;
using PinService.Application.Behaviors;
using PinService.Application.USAZCTAAggregate.Dtos;
using PinService.Application.USAZCTAAggregate.Queries;
using PinService.Domain.Core.Interfaces;
using PinService.Infrastructure;
using PinService.Infrastructure.EntityConfigurations;
using PinService.Infrastructure.Repositories;
using PinService.WebApi.HealthChecks;

namespace PinService.WebApi
{
    public static class Extensions
    {


        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {

            var healthBuilder = services.AddHealthChecks();

            services.AddHealthChecksUI()
                .AddInMemoryStorage();

            healthBuilder.Add
                (
                new HealthCheckRegistration("DbHealthCheck", factory: (serv) => new DbHealthCheck(configuration["ConnectionStrings:Default"], "SELECT 1"), HealthStatus.Unhealthy, null)
                );
            return services;
        }
    }
}
