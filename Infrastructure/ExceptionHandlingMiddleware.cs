using System.Net;
using Core.Models;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        var statusCode = "Error";
        var message = "Unhandled exception occured.";
        switch(exception)
        {
            case KeyNotFoundException:
                message = exception.Message;
                break;
            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized.ToString();
                message = exception.Message;
                break;
            case ForbiddenException:
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                message = exception.Message;
                break;
        }

        var response = new Response(statusCode.ToString(), message);

        await context.Response.WriteAsJsonAsync(response);
    }
    
}