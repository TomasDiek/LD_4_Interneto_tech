using LD_4_Interneto_tech.Middlewares;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;

using System.Net;
namespace LD_4_Interneto_tech.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {

        
        public static void ConfigureExceptionHandler(this IApplicationBuilder app,
         IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
        
        public static void ConfigureBuiltInExceptionHandler(this IApplicationBuilder app,IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(
                    options => {
                        options.Run(
                            async context =>
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                var ex = context.Features.Get<IExceptionHandlerFeature>();
                                if (ex != null)
                                {
                                    await context.Response.WriteAsync(ex.Error.Message);
                                }
                            }
                        );
                    }
                );
            }
        }
    }
}
