using backend.Application.Commons.Bases;
using backend.Application.Commons.Exceptions;
using backend.Utilities.Static;
using System.Text.Json;

namespace backend.Api.Middleware;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (ValidationException ex)
        {
            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, new BaseResponse<object>
            {
                IsSuccess = false,
                Message = ReplyMessage.MESSAGE_VALIDATE,
                Errors = ex.Errors
            });
        }
    }
}