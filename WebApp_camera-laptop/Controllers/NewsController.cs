using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Linq;
using WebApp_camera_laptop.Models;

namespace WebApp_camera_laptop.Controllers
{
    public class NewsController : Controller
    {
        private readonly webap_camera_laptopContext _context;

        public NewsController(webap_camera_laptopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("/baiviet/{Alias}-{NewId}.html", Name = "NewsDetails")]
        public IActionResult Details(int NewId)
        {
            try
            {
                var baiviet = _context.News
                    .Include(x => x.Cat)
                    .FirstOrDefault(x => x.NewId == NewId);
                if (baiviet == null)
                {
                    return RedirectToAction("Index");
                }
                var Isbaiviets = _context.News
                   .AsNoTracking()
                   .Where(x => x.NewId != NewId && x.Published == true && x.IsHot == true)
                   .OrderByDescending(x => x.NewId)
                   .Take(4)
                   .ToList();
                ViewBag.baiviethot = Isbaiviets;
                ViewBag.Namebaiviet = baiviet;
                return View(baiviet);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }


        [Route("/baiviet/{Alias}", Name = "Listbaiviet")]
        public IActionResult List(string Alias, int page = 1)
        {
            try
            {
                var pageSize = 12;
                var danhmuc = _context.CategoriesNews.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);
                var baiviets = _context.News
                    .AsNoTracking()
                    .Where(p => p.CatId == danhmuc.CatNewId && p.Published == true)
                    .OrderByDescending(x => x.NewId);
                PagedList<News> models = new PagedList<News>(baiviets.AsQueryable(), page, pageSize);
                var baiviethot = _context.News
                    .AsNoTracking()
                    .Where(x => x.IsHot == true && x.Published == true)
                    .OrderByDescending(x => x.NewId)
                    .Take(4)
                    .ToList();
                ViewBag.Baiviethot = baiviethot;
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = danhmuc;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }

    }
}
