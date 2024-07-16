using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Exceptions;

public class ExceptionMiddeware : IMiddleware
{
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
		try
		{
			await next(httpContext);
		}
		catch (Exception ex)
		{
			await HandleExceprionAsync(httpContext, ex);
		}
    }

    private static Task HandleExceprionAsync(HttpContext httpContext, Exception ex)
    {
		int statusCode = GetStatusCode(ex);
		httpContext.Response.ContentType = "application/json";
		httpContext.Response.StatusCode = statusCode;

		List<string> errors = new()
		{
			ex.Message,
			ex.InnerException?.ToString()
		};

		return httpContext.Response.WriteAsync(new ExceptionModel
		{
			Errors = errors,
			StatusCode = statusCode
		}.ToString());
    }

	private static int GetStatusCode(Exception exception) =>
		exception switch
		{
			BadRequestException => StatusCodes.Status400BadRequest,
			NotFoundException => StatusCodes.Status400BadRequest,
			ValidationException => StatusCodes.Status422UnprocessableEntity,
			_ => StatusCodes.Status500InternalServerError
		};
	
}
