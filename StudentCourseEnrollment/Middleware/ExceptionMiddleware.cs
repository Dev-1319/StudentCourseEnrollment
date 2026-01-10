using StudentCourseEnrollment.DTOs;
using System.Net;

using System.Text.Json;

namespace StudentCourseEnrollment.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<Exception> _logger;
        private readonly IHostEnvironment _environment;
        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<Exception> logger, IHostEnvironment hostEnvironment ) 
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
            _environment = hostEnvironment;

        }
 
    
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
                catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message); // Log the error for the developers
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new ErrorResponse
                {
                    ErrorCode = httpContext.Response.StatusCode,
                    Message = "Internal Server Error. Please try again later.",
                    Details = _environment.IsDevelopment() ? ex.StackTrace?.ToString() : null
                };

                var json = JsonSerializer.Serialize(response);
                await httpContext.Response.WriteAsync(json);
            }
                


        }
    }
}
