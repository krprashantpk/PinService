using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PenService.WebApi.Middlewares
{
    public class HandleExceptionMiddleWare 
    {
        private readonly RequestDelegate next;

        public HandleExceptionMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var bytes = UTF8Encoding.UTF8.GetBytes(e.Message);
                await context.Response.Body.WriteAsync(bytes, 0, bytes.Count());
            }


        }
    }


    public static class HandleExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HandleExceptionMiddleWare>();
        }
    }
}
