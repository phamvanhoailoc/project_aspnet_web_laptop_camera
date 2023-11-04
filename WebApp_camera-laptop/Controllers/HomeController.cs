using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp_camera_laptop.Models;
using WebApp_camera_laptop.ModelViews;

namespace WebApp_camera_laptop.Controllers
{
    public class HomeController : Controller
    {
        private readonly webap_camera_laptopContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, webap_camera_laptopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                HomeViewVM model = new HomeViewVM();

                var danhmuc = _context.Categories.AsNoTracking().OrderBy(x => x.CatId).ToList();
                var laptops = _context.Products
                    .AsNoTracking()
                    .Where(x => x.HomeFlag == true && x.ProductCategoris.Any(pc => pc.CatId == 33) && x.Active == true)
                    .OrderByDescending(x => x.ProductId)
                    .ToList();
                var camera = _context.Products
                    .AsNoTracking()
                    .Where(x => x.HomeFlag == true && x.ProductCategoris.Any(pc => pc.CatId == 32) && x.Active == true)
                    .OrderByDescending(x => x.ProductId)
                    .ToList();
                var maybo = _context.Products
                    .AsNoTracking()
                    .Where(x => x.HomeFlag == true && x.ProductCategoris.Any(pc => pc.CatId == 35) && x.Active == true)
                    .OrderByDescending(x => x.ProductId)
                    .ToList();
                var mayvanphong = _context.Products
                    .AsNoTracking()
                    .Where(x => x.HomeFlag == true && x.ProductCategoris.Any(pc => pc.CatId == 37) && x.Active == true)
                    .OrderByDescending(x => x.ProductId)
                    .ToList();
                var linhkienmaytinh = _context.Products
                    .AsNoTracking()
                    .Where(x => x.HomeFlag == true && x.ProductCategoris.Any(pc => pc.CatId == 34) && x.Active == true)
                    .OrderByDescending(x => x.ProductId)
                    .ToList();
                var thietbianninh = _context.Products
                    .AsNoTracking()
                    .Where(x => x.HomeFlag == true && x.ProductCategoris.Any(pc => pc.CatId == 38) && x.Active == true)
                    .OrderByDescending(x => x.ProductId)
                    .ToList();
                var thietbimang = _context.Products
                    .AsNoTracking()
                    .Where(x => x.HomeFlag == true && x.ProductCategoris.Any(pc => pc.CatId == 40) && x.Active == true)
                    .OrderByDescending(x => x.ProductId)
                    .ToList();
                var duan = _context.News
                    .AsNoTracking()
                    .Where(x => x.IsNewfeed == true && x.CatId == 1 && x.Published == true)
                    .OrderByDescending(x => x.NewId)
                    .ToList();

                //model.Products = IsProducts;
                model.SetDefaultThumbValues();
                model.Categories = danhmuc;
                ViewBag.danhmuc = danhmuc;
                ViewBag.laptops = laptops;
                ViewBag.camera = camera;
                ViewBag.maybo = maybo;
                ViewBag.mayvanphong = mayvanphong;
                ViewBag.linhkienmaytinh = linhkienmaytinh;
                ViewBag.thietbianninh = thietbianninh;
                ViewBag.thietbimang = thietbimang;
                ViewBag.duan = duan;
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.ErrorMessage = "Xin lỗi, trang bạn đang tìm kiếm không tồn tại.";
            return View();
        }

        [Route("Lien-he.html", Name = "Liên hệ")]
        public IActionResult Contact()
        {

            return View();
        }

        

    }
}
