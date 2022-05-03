using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PenService.Application.USAZCTAAggregate.Queries;
using FluentValidation.AspNetCore;
using PenService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PenService.Infrastructure.EntityConfigurations;
using PenService.Application.Behaviors;
using PenService.Application.USAZCTAAggregate.Services;
using PenService.Infrastructure.Repositories;

namespace PenService.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigurePackages(this IServiceCollection services)
        {
            services.AddMediatR(typeof(SearchNearByZctasByRadius), typeof(Program));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(SearchNearByZctasByRadiusValidator)));
            return services;
        }

        public static IServiceCollection ConfigureApplicationService(this IServiceCollection services)
        {

            services.AddScoped<IUSAZctaService, USAZctaService>();



            services.AddFluentValidation();
            return services;

        }

        public static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository, GenericRepository>();
            return services;

        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services)
        {
            services.AddDbContext<PinServiceContext>(
                x => x.UseSqlServer("server=localhost,1433;user id=sa;password=SQLpk@123;database=PinService",
                                builder => builder
                                .MigrationsAssembly(typeof(USAZCTAEntityTypeConfiguration).Assembly.FullName)
                                ));
            return services;

        }
    }
}
