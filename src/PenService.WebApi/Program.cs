using PenService.WebApi.Middlewares;

namespace PenService.WebApi;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        builder.Services.AddControllers(options =>
        {
            //options.RespectBrowserAcceptHeader = true;
        })
            //.AddXmlSerializerFormatters();
            ;

        builder.Services.ConfigurePackages();
        builder.Services.ConfigureApplicationService();
        builder.Services.ConfigureRepository();
        builder.Services.ConfigureDatabase();
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            //app.UseLogging();
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
