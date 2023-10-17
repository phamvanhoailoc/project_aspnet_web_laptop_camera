using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Linq;
using WebApp_camera_laptop.Models;

namespace WebApp_camera_laptop.Controllers
{

    public class ProductsController : Controller
    {
        private readonly webap_camera_laptopContext _context;

        public ProductsController(webap_camera_laptopContext context)
        {
            _context = context;
        }

    
        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 12;
                var IsProducts = _context.Products
                    .AsNoTracking()
                    .OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(IsProducts, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                var IsBestsell = _context.Products
                    .AsNoTracking()
                    //.Where(x => x.BestSellers && x.BestSellers == true)
                    .OrderByDescending(x => x.DateCreated)
                    .Take(4)
                    .ToList();
                ViewBag.BestSell = IsBestsell;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Route("/{Alias}", Name = "ListProduct")]
        public IActionResult List(string Alias, int page = 1)
        {
            try
            {
                var pageSize = 12;
                var danhmuc = _context.Categories.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);
                var IsProducts = _context.Products
                    .AsNoTracking()
                    .Where(p => p.ProductCategoris.Any(pc => pc.CatId == danhmuc.CatId) && p.Active == true)
                    .OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(IsProducts.AsQueryable(), page, pageSize);
                var IsBestsell = _context.Products
                    .AsNoTracking()
                    .Where(x => x.BestSellers == true && x.Active == true)
                    .OrderByDescending(x => x.DateCreated)
                    .Take(4)
                    .ToList();
                ViewBag.BestSell = IsBestsell;
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = danhmuc;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [Route("/{Alias}-{ProductId}.html", Name = "ProductDetails")]
        public IActionResult Details(int ProductId)
        {
            try
            {
                var product = _context.Products
                   // .Include(x => x.Cat)
                    .Include(x => x.Producer)
                    .Include(x => x.Status)
                    .FirstOrDefault(x => x.ProductId == ProductId);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                var IsProduct = _context.Products
                   .AsNoTracking()
                   .Where(x => x.CatId == product.CatId && x.ProductId != ProductId && x.Active == true)
                   .OrderByDescending(x => x.DateCreated)
                   .Take(4)
                   .ToList();
                ViewBag.sanpham = IsProduct;
                return View(product);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public IActionResult Error()
        {
            // Xử lý lỗi ở đây (nếu cần)
            return View(); // Chuyển hướng đến view "Error"
        }
    }
}
