using MiddleWare2.ErrorInfo;
using ReelTalkReviews.ErrorInfo;
using ReelTalkReviews.Middleware;

namespace ReelTalkReviews.Middleware
{
    public class ExceptionHandling :IMiddleware
    {
        private readonly ILogger<ExceptionHandling> _logger;
        public ExceptionHandling(ILogger<ExceptionHandling> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            try
            {
                await next(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleException(context, ex);
            }
        }
        private static Task HandleException(HttpContext context, Exception ex)
        {
            int statusCode;
            switch (ex)
            {
                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    break;
                case BadRequestException:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
                case UnAuthorizedException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    break;
                case ForbiddenException:
                    statusCode = StatusCodes.Status403Forbidden;
                    break;
             
                default :
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;
            }
            var error = new Error
            {
                StatusCode = statusCode,
                ErrorMessage = ex.Message,
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.StatusCode;
            return context.Response.WriteAsync(error.ToString());
        }

    }


    public static class ExceptionHandlingExtension
    {
        public static IApplicationBuilder ExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandling>();
        }
    }
}

