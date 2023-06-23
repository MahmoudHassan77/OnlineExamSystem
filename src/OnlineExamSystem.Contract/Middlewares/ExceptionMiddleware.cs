using System.Net;
using OnlineExamSystem.Common.Exceptions;
using Newtonsoft.Json;
using OnlineExamSystem.Common.Dtos;
using Microsoft.AspNetCore.Http;

namespace OnlineExamSystem.Common.Middlewares;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var (statusCode, result) = exception switch
        {
            BadRequestException badRequestException => (HttpStatusCode.BadRequest, CreateErrorResult(new List<string> { badRequestException.Message }, "Bad Request", (int)HttpStatusCode.BadRequest)),
            ValidationException validationException => (HttpStatusCode.BadRequest, CreateErrorResult(validationException.Errors, "Validation Error", (int)HttpStatusCode.BadRequest)),
            NotFoundException notFoundException => (HttpStatusCode.NotFound, CreateErrorResult(new List<string> { notFoundException.Message }, "NotFound Error", (int)HttpStatusCode.NotFound)),
            _ => (HttpStatusCode.InternalServerError, CreateErrorResult(new List<string> {exception.Message}, "Failure", (int)HttpStatusCode.InternalServerError) )
        };

        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(result);
    }
    private static string CreateErrorResult(List<string> errors, string type, int code) => JsonConvert.SerializeObject(new ErrorResponse(errors, type, code));
}
