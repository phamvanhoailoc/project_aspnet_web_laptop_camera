using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using WebApp_camera_laptop.Models;

namespace WebApp_camera_laptop.Views.Shared.Components.CategoryMenuViewComponent
{
    [ViewComponent]
    public class CategoryMenu : ViewComponent
    {
        private readonly webap_camera_laptopContext _context;

        public CategoryMenu(webap_camera_laptopContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            // Lấy danh sách danh mục từ dữ liệu (Model.Categories) hoặc từ nguồn dữ liệu khác
            var danhmuc = _context.Categories.AsNoTracking().OrderBy(x => x.CatId).ToList();
            return View(danhmuc);
        }
    }
}

