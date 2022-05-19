using FluentValidation.AspNetCore;
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
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigurePackages(this IServiceCollection services)
        {
            services.AddMediatR(typeof(SearchNearByZctasByRadius), typeof(Program));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(SearchNearByZctasByRadiusValidator)));
            services.AddAutoMapper(typeof(USAZctaDto).Assembly);
            return services;
        }

        public static IServiceCollection ConfigureApplicationService(this IServiceCollection services)
        {

            services.AddScoped<ICacheService, CacheService>();

            services.AddFluentValidation();
            return services;

        }

        public static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository, GenericRepository>();
            return services;

        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PinServiceContext>(
                x => x.UseSqlServer(configuration["ConnectionStrings:Default"],
                                builder => builder
                                .MigrationsAssembly(typeof(USAZCTAEntityTypeConfiguration).Assembly.FullName)
                                ));
            return services;

        }


        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {

            var healthBuilder = services.AddHealthChecks();

            services.AddHealthChecksUI()
                .AddInMemoryStorage()
;


            healthBuilder.Add
                (
                new HealthCheckRegistration("DbHealthCheck", factory: (serv) => new DbHealthCheck(configuration["ConnectionStrings:Default"], "SELECT 1"), HealthStatus.Unhealthy, null)
                );
            return services;
        }
    }
}
