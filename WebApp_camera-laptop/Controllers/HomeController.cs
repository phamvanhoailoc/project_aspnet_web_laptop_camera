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
                int CatId = 11;
                HomeViewVM model = new HomeViewVM();
                List<Category> categories = _context.Categories.ToList();
                List<Category> hierarchicalCategories = BuildCategoryHierarchy(categories);
                var danhmuc = _context.Categories.AsNoTracking().SingleOrDefault(x => x.CatId == CatId);
                var IsProducts = _context.Products
                    .AsNoTracking()
                    .Where(p => p.ProductCategoris.Any(pc => pc.CatId == danhmuc.CatId))
                    .OrderByDescending(x => x.DateCreated)
                    .ToList(); 

                model.Products = IsProducts;
                model.SetDefaultThumbValues();
                model.Categories = hierarchicalCategories;
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        private List<Category> BuildCategoryHierarchy(List<Category> categories)
        {
            var categoryLookup = categories.ToDictionary(c => c.CatId);

            foreach (var category in categories)
            {
                if (category.ParentId != null)
                {
                    var parentCategory = categoryLookup[category.ParentId.Value];
                    parentCategory.Subcategories ??= new List<Category>();
                    parentCategory.Subcategories.Add(category);
                }
            }

            // Tìm danh mục gốc (laptop và camera) và loại bỏ chúng
            var rootCategories = categories.Where(c => c.ParentId != null).ToList();

            return rootCategories;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
