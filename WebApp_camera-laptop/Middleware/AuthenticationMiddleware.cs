using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApp_camera_laptop.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Kiểm tra đăng nhập ở đây
            var taikhoanID = context.Session.GetString("Id");

            // Lấy tuyến đường được yêu cầu
            var requestPath = context.Request.Path;

            // Kiểm tra xem yêu cầu có đi qua tuyến đường "areas" hay không
            if (requestPath.StartsWithSegments("/Admin"))
            {
                // Nếu yêu cầu đi qua tuyến đường "areas", kiểm tra xác thực
                if (taikhoanID == null)
                {
                    // Chuyển hướng đến trang đăng nhập
                    context.Response.Redirect("/Login");
                    return;
                }
            }

            // Nếu đã đăng nhập hoặc yêu cầu không đi qua tuyến đường "areas", tiếp tục xử lý yêu cầu
            await _next(context);
        }
    }
}
