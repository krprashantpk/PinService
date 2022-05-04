using PenService.Domain.Core.Logging;
using PenService.Domain.Core.MediatR;
using PenService.Domain.Core.Seed;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PenService.WebApi.Middlewares
{

    /// <summary>
    /// See : https://stackoverflow.com/questions/63190975/httprequest-does-not-contain-a-definition-for-enablerewind
    /// </summary>
    public class LoggingRequestMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<LoggingRequestMiddleWare> logger;

        public LoggingRequestMiddleWare(RequestDelegate next, ILogger<LoggingRequestMiddleWare> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var injectedResponseBody = new MemoryStream();
            var originalResponseBodyReference = context.Response.Body;
            var watch = new Stopwatch();
            watch.Start();
            try
            {
                var requestResponse = new RequestResponse();
                context.Response.Body = injectedResponseBody;

                context.Request.EnableBuffering();
                var bodyReader = new StreamReader(context.Request.Body);
                var bodyAsText = await bodyReader.ReadToEndAsync();

                requestResponse.Request = new Request(context.Request.Method,
                 context.Request.Path,
                 context.Request.QueryString.ToString(), bodyAsText,
                 context.Request.Headers
                         .ToDictionary(kvp => kvp.Key,
                                 kvp => kvp.Value.Aggregate((x, y) => x + ";" + y)));


                await next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                await injectedResponseBody.CopyToAsync(originalResponseBodyReference);
                

                watch.Stop();
                requestResponse.Response = new Response(watch.ElapsedMilliseconds,
                    context.Response.StatusCode,
                    responseBody, context.Response.Headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Aggregate((x, y) => x + ";" + y)));


                
                logger.LogInformation("Request Log : {0} {1} ", requestResponse.Request.Resource, requestResponse.Response.Body);
            }
            finally
            {
                
                injectedResponseBody?.Dispose();
            }

        }

    }


    public static class HandleExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggingRequestMiddleWare>();
        }
    }
}
