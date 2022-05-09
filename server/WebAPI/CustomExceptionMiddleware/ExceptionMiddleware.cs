using GlobalErrorHandling.Models;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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
        catch (KeyNotFoundException invEx)
        {
            Log.Error($"Item not found: {invEx}");
            await HandleExceptionAsync(httpContext, invEx);
        }
        catch (InvalidOperationException invEx)
        {
            Log.Error($"Invalid action: {invEx}");
            await HandleExceptionAsync(httpContext, invEx);
        }
        catch (UnauthorizedAccessException authEx)
        {
            Log.Error($"Unauthorized action: {authEx}");
            await HandleExceptionAsync(httpContext, authEx);
        }
        catch (AccessViolationException avEx)
        {
            Log.Error($"A new violation exception has been thrown: {avEx}");
            await HandleExceptionAsync(httpContext, avEx);
        }
        catch (Exception ex)
        {
            Log.Error($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = exception.GetType().Name switch
        {
            "KeyNotFoundException" => (int)HttpStatusCode.NotFound,
            "InvalidOperationException" => (int)HttpStatusCode.Conflict,
            "UnauthorizedAccessException" => (int)HttpStatusCode.Unauthorized,
            "AccessViolationException" => (int)HttpStatusCode.InternalServerError,
            _ => (int)HttpStatusCode.InternalServerError,
        };

        var message = exception.GetType().Name switch
        {
            "KeyNotFoundException" => "Item not found",
            "InvalidOperationException" => "Conflict generated with body data",
            "UnauthorizedAccessException" => "Unathorized action with logged user",
            "AccessViolationException" => "Access violation error from the custom middleware",
            _ => "Internal Server Error from the custom middleware.",
        };

        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = message
        }.ToString()); ;
    }

}