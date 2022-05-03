
using Microsoft.AspNetCore;
using PenService.WebApi.Middlewares;
using System;

namespace PenService.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();

        builder.Services.ConfigurePackages();
        builder.Services.ConfigureApplicationService();
        builder.Services.ConfigureRepository();
        builder.Services.ConfigureDatabase();
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddSwaggerGen();

        //builder.Services.AddTransient<HandleExceptionMiddleWare>();

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            //app.UseDeveloperExceptionPage();
            app.UseExceptionHandling();
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
            });
        app.Run();
    }
}
