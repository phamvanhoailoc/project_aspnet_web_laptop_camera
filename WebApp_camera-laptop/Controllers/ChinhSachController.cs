using Microsoft.AspNetCore.Mvc;

namespace WebApp_camera_laptop.Controllers
{
    public class ChinhSachController : Controller
    {
        [Route("Chinh-sach-doi-tra-hang.html", Name = "Chính sách đổi trả hàng")]
        public IActionResult PageChinhSachDoiTra()
        {
            return View();
        }

        [Route("Chinh-sach-bao-hanh.html", Name = "Chính sách bảo hành")]
        public IActionResult PageChinhSachBaoHanh()
        {
            return View();
        }

    }
}
