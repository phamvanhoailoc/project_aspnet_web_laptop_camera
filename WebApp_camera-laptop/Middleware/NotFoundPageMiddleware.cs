using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebApp_camera_laptop.Middleware
{
    public class NotFoundPageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<NotFoundPageMiddleware> _logger;

        public NotFoundPageMiddleware(RequestDelegate next, ILogger<NotFoundPageMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                _logger.LogInformation($"404 Not Found: {context.Request.Path}");
                context.Request.Path = "/error"; // Điều hướng đến trang lỗi tùy chỉnh
                await _next(context);
            }
        }
    }
}
