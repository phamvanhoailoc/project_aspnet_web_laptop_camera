using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            HomeViewVM model = new HomeViewVM();
            List<Category> categories = _context.Categories.ToList();
            List<Category> hierarchicalCategories = BuildCategoryHierarchy(categories);


            model.Categories = hierarchicalCategories;
            return View(model);
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
