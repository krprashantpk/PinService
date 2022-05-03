
using Microsoft.AspNetCore;
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
        builder.Services.ConfigureDatabase();
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
            });
        app.Run();
    }
}
