using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using WebApp_camera_laptop.Areas.Admin.ModelViews;
using WebApp_camera_laptop.Helpper;
using WebApp_camera_laptop.Models;

namespace WebApp_camera_laptop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminNewsController : Controller
    {
        private readonly webap_camera_laptopContext _context;
        public INotyfService _notyfService { get; }

        public AdminNewsController(webap_camera_laptopContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminNews
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var baiviet = _context.News
            .AsNoTracking()
            .Include(t => t.Cat);

            PagedList<News> models = new PagedList<News>(baiviet, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;

            return View(models);
        }

        // GET: Admin/AdminNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Account)
                .FirstOrDefaultAsync(m => m.NewId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/AdminNews/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.CategoriesNews, "CatNewId", "CatName");
            return View();
        }

        // POST: Admin/AdminNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsAdminViewModel baiviet, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (fThumb != null)
                    {
                        List<IFormFile> files = Request.Form.Files.ToList();
                        if (files != null && files.Count() < 2)
                        {
                            List<string> uploadedFileNames = new List<string>();
                            int imageIndex = 1;
                            foreach (var file in files)
                            {
                                string extension = Path.GetExtension(file.FileName);
                                string baseImageName = Utilities.SEOUrl(baiviet.Title);
                                string image = baseImageName + "_" + imageIndex.ToString() + extension;

                                string uploadedFileName = await Utilities.UploadFile(file, @"news", image);
                                uploadedFileNames.Add(uploadedFileName);
                                imageIndex++;
                            }
                            string thumbString = string.Join(",", uploadedFileNames);
                            baiviet.Thumb = thumbString;
                        }
                        else
                        {
                            _notyfService.Error("Không được thêm quá 1 tấm ảnh !!!");
                            return View(baiviet);
                        }

                    }
                    else
                    {
                        _notyfService.Error("Phải thêm tối thiểu 1 ảnh !!!");
                        return View(baiviet);
                    }
                    baiviet.Alias = Utilities.SEOUrl(baiviet.Title);
                    baiviet.CreatedDate = DateTime.Now;


                    News news = new News
                    {
                        Title = baiviet.Title,
                        Thumb = baiviet.Thumb,
                        Alias = baiviet.Alias,
                        CatId = baiviet.CatId,
                        CreatedDate = baiviet.CreatedDate,
                        Scontents = baiviet.Scontents,
                        Contents = baiviet.Contents,
                        IsHot = baiviet.IsHot,
                        IsNewfeed = baiviet.IsNewfeed,
                        Published = baiviet.Published,
                        Tags = baiviet.Tags,
                        MetaDesc = baiviet.MetaDesc,
                        MetaKey = baiviet.MetaKey,
 
                    };
                    _context.Add(news);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Tạo sản phẩm thành công");

                    // Sử dụng DbContext.Entry để lấy đối tượng sau khi đã thêm vào cơ sở dữ liệu
                    var addednews = _context.Entry(news).Entity;

                    
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CatId"] = new SelectList(_context.CategoriesNews, "CatNewId", "CatName", baiviet.CatId);
                return View(baiviet);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // GET: Admin/AdminNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", news.AccountId);
            return View(news);
        }

        // POST: Admin/AdminNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] News news)
        {
            if (id != news.NewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.NewId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", news.AccountId);
            return View(news);
        }

        // GET: Admin/AdminNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Account)
                .FirstOrDefaultAsync(m => m.NewId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Admin/AdminNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.NewId == id);
        }
    }
}
