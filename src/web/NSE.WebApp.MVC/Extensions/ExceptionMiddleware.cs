using System.Net;
using Polly.CircuitBreaker;

namespace NSE.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomHttpResponseException exception)
            {
                HandleRequestExceptionAsync(context, exception);
            }
            catch (BrokenCircuitException)
            {
                HandleRequestBrokenCircuitExceptionAsync(context);
            }

        }

        private void HandleRequestBrokenCircuitExceptionAsync(HttpContext context)
        {
            context.Response.Redirect($"/sistema-indisponivel");
        }

        private static void HandleRequestExceptionAsync(HttpContext context, CustomHttpResponseException exception)
        {
            if (exception.StatusCode == HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
                return;
            }
            context.Response.StatusCode = (int)exception.StatusCode;
        }
    }
}
