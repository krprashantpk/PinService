using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PinService.Domain.Core.Interfaces;
using PinService.Infrastructure.EntityConfigurations;
using PinService.Infrastructure.Repositories;

namespace PinService.Infrastructure;


public static class Extensions
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PinServiceContext>(
            x => x.UseSqlServer(configuration["ConnectionStrings:SqlServer"],
                            builder => builder
                            .MigrationsAssembly(typeof(USAZCTAEntityTypeConfiguration).Assembly.FullName)
                            ));



        services.AddScoped<IGenericRepository, GenericRepository>();
        return services;
    }
}