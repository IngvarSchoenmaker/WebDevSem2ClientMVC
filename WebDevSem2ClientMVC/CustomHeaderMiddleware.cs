using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebDevSem2ClientMVC
{
    public class CustomHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            await _next(context);
        }
    }
}
