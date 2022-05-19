
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PinService.Application.Behaviors;
using PinService.Application.USAZCTAAggregate.Dtos;
using PinService.Application.USAZCTAAggregate.Queries;
using PinService.Application.USAZCTAAggregate.Services;
using PinService.Domain.Core.Interfaces;
using PinService.Infrastructure;

namespace PinService.Application;


public static class Extensions
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        #region  AddServices
        services.AddTransient<IUSAZctaService, USAZctaService>();
        services.AddTransient<IUnitOfWork<PinServiceContext>, UnitOfWork<PinServiceContext>>();


        #endregion

        #region AddPackage


        services.AddMediatR(typeof(SearchNearByZctasByRadius));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(SearchNearByZctasByRadiusValidator)));
        services.AddAutoMapper(typeof(USAZctaDto).Assembly);
        #endregion

        return services;
    }

}