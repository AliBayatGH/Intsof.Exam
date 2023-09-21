using Application.Common.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Intsoft.Exam.API.Middlewares;
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = ex switch
            {
                BadRequestException e =>
                    (int)HttpStatusCode.BadRequest,

                NotFoundException e =>
                    (int)HttpStatusCode.NotFound,

                _ => (int)HttpStatusCode.InternalServerError
            };

            var result = JsonConvert.SerializeObject(new
            {
                message = ex.Message,
            });

            await response.WriteAsync(result);
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
