using MySocialApp.Application;
using MySocialApp.Infrastructure.Exceptions;
using System.Net;

namespace MySocialApp.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
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
            LogException(httpContext, ex);
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private void LogException(HttpContext context, Exception exception)
    {
        if (exception is DomainException ||
            exception is NotFoundException ||
            exception.InnerException is DomainException ||
            exception.InnerException is NotFoundException)
        {
            return;
        }

        _logger.LogError(exception, "NotHandledException");
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = new ErrorResponse();
        var statusCode = new HttpStatusCode();

        GenerateResponse(exception, ref response, ref statusCode);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(response.ToString());
    }

    private void GenerateResponse(Exception exception, ref ErrorResponse errorResponse, ref HttpStatusCode statusCode)
    {
        string message = string.Empty;
        IDictionary<string, string[]> errorList = new Dictionary<string, string[]>();

        if (exception is DomainException || exception.InnerException is DomainException)
        {
            statusCode = HttpStatusCode.UnprocessableEntity;
            errorResponse.Message = exception.Message;
            errorResponse.ErrorList = ((DomainException)exception).errors;
        }
        else if (exception is NotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
            errorResponse.Message = exception.Message;
            errorResponse.ErrorList = null;
        }
        else
        {
            statusCode = HttpStatusCode.InternalServerError;
            errorResponse.Message = exception.Message;
            errorResponse.ErrorList = null;
        }
    }
}
