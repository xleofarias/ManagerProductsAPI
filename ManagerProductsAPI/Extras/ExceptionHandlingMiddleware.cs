namespace ManagerProductsAPI.Extras
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
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
                Console.WriteLine($"Unhandled Exception: {ex.Message}");
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync("An unexpected error occurred.");
            }
        }
    }

}
