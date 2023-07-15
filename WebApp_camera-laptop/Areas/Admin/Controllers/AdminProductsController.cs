using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DiChoSaiGon.Helpper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using WebApp_camera_laptop.Models;

namespace WebApp_camera_laptop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly webap_camera_laptopContext _context;

        public AdminProductsController(webap_camera_laptopContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminProducts
        public async Task<IActionResult> Index(int page = 1, int CatID = 0)
        {
            try {

              


                var pageNumber = page;
                var pageSize = 8;
                List<Product> IsProducts = new List<Product>();
                if (CatID != 0)
                {
                    IsProducts = _context.Products
                        .AsNoTracking()
                        .Where(x => x.CatId == CatID)
                       // .Include(x => x.Cat)
                        .Include(x => x.Status)
                        .Include(x => x.Producer)
                        .OrderByDescending(x => x.ProductId).ToList();
                }
                else
                {
                    IsProducts = _context.Products
                        .AsNoTracking()
                        //.Include(x => x.Cat)
                        .Include(x => x.Status)
                        .Include(x => x.Producer)
                        .OrderByDescending(x => x.ProductId).ToList();
                }

                PagedList<Product> models = new PagedList<Product>(IsProducts.AsQueryable(), pageNumber, pageSize);
                ViewBag.CurrentCateID = CatID;
                ViewBag.CurrentPage = pageNumber;
                ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName", CatID);
                return View(models);
            } catch (Exception ex) { 
                return View("Error"); 
            }
            
        }

        // GET: Admin/AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try {
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Products
                    //.Include(p => p.Cat)
                    .Include(p => p.Producer)
                    .Include(p => p.Status)
                    .FirstOrDefaultAsync(m => m.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            } catch (Exception ex) { 
                return View("Error"); 
            }
            
        }

        // GET: Admin/AdminProducts/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName");
            ViewData["ProducerId"] = new SelectList(_context.Producers, "ProducerId", "ProducerName");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusName");
            return View();
        }

        // POST: Admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ShortDesc,Description,CatId,Price,Discount,Thumb,Video,DateCreated,DateModified,BestSellers,HomeFlag,Active,Tags,Title,Alias,MetaDesc,MetaKey,UnitslnStock,ProducerId,StatusId,Cpu,Ram,HardDrive,GraphicCard,Screen,Location,TypeCameraId,Resoluton,CameraAngle,Storege,FarInfraredVision,DeviceSupport,ControlPhone,Utilities,Conversation,Adapter,InputPower,Size")] Product product, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                if (fThumb != null)
                {
                    List<IFormFile> files = Request.Form.Files.ToList();
                    List<string> uploadedFileNames = new List<string>();
                    int imageIndex = 1;
                    foreach (var file in files)
                    {
                        string extension = Path.GetExtension(file.FileName);
                        string baseImageName = Utilities.SEOUrl(product.ProductName);
                        string image = baseImageName + "_" +imageIndex.ToString() +  extension;

                        string uploadedFileName = await Utilities.UploadFile(file, @"products", image);
                        uploadedFileNames.Add(uploadedFileName);
                        imageIndex++;
                    }
                    string thumbString = string.Join(",", uploadedFileNames);
                    product.Thumb = thumbString;
                    product.Alias = Utilities.SEOUrl(product.ProductName);
                    product.DateModified = DateTime.Now;
                    product.DateCreated = DateTime.Now;
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", product.CatId);
            ViewData["ProducerId"] = new SelectList(_context.Producers, "ProducerId", "ProducerId", product.ProducerId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", product.StatusId);
            return View(product);
        }

        // GET: Admin/AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try {
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", product.CatId);
                ViewData["ProducerId"] = new SelectList(_context.Producers, "ProducerId", "ProducerId", product.ProducerId);
                ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", product.StatusId);
                return View(product);
            } catch (Exception ex) { return View("Error"); }
            
        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ShortDesc,Description,CatId,Price,Discount,Thumb,Video,DateCreated,DateModified,BestSellers,HomeFlag,Active,Tags,Title,Alias,MetaDesc,MetaKey,UnitslnStock,ProducerId,StatusId,Cpu,Ram,HardDrive,GraphicCard,Screen,Location,TypeCameraId,Resoluton,CameraAngle,Storege,FarInfraredVision,DeviceSupport,ControlPhone,Utilities,Conversation,Adapter,InputPower,Size")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", product.CatId);
            ViewData["ProducerId"] = new SelectList(_context.Producers, "ProducerId", "ProducerId", product.ProducerId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", product.StatusId);
            return View(product);
        }

        // GET: Admin/AdminProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
               // .Include(p => p.Cat)
                .Include(p => p.Producer)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
