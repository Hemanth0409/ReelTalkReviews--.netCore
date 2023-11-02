using MiddleWare2.ErrorInfo;
using ReelTalkReviews.ErrorInfo;
using ReelTalkReviews.Middleware;

namespace ReelTalkReviews.Middleware
{
    public class ExceptionHandling :IMiddleware
    {
     
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            try
            {
                await next(context);

            }
            catch (Exception ex)
            {
              
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
                case SuccessException:
                    statusCode = StatusCodes.Status200OK;
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

