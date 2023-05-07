using BAHelper.BLL.Exceptions;
using System.Net;

namespace BAHelper.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error) 
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = error switch
                {
                    ExistUserException => (int)HttpStatusCode.Unauthorized,
                    InvalidTokenException => (int)HttpStatusCode.Unauthorized,
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    UserNotFoundException => (int)HttpStatusCode.NotFound,
                    InvalidUserNameOrPasswordException => (int)HttpStatusCode.Unauthorized,
                    NoAccessException => (int)HttpStatusCode.Unauthorized,
                    _=> (int)HttpStatusCode.InternalServerError
                };
                await context.Response.WriteAsync(error.Message);
            }
        }
    }
}
