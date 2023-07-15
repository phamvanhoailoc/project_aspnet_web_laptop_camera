using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp_camera_laptop.Models;

namespace WebApp_camera_laptop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductCategorisController : Controller
    {
        private readonly webap_camera_laptopContext _context;

        public AdminProductCategorisController(webap_camera_laptopContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminProductCategoris
        public async Task<IActionResult> Index()
        {
            try
            {
                var webap_camera_laptopContext = _context.ProductCategoris
                .Include(p => p.Cat)
                .Include(p => p.Products);
                ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName");
                ViewData["products"] = new SelectList(_context.Products, "ProductId", "ProductName");
                return View(await webap_camera_laptopContext.ToListAsync());
            }
            catch (Exception ex) {
                return View("Error");
            }
           
        }

        // GET: Admin/AdminProductCategoris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategori = await _context.ProductCategoris
                .Include(p => p.Cat)
                .Include(p => p.Products)
                .FirstOrDefaultAsync(m => m.ProductCatId == id);
            if (productCategori == null)
            {
                return NotFound();
            }

            return View(productCategori);
        }

        // GET: Admin/AdminProductCategoris/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName");
            ViewData["ProductsId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: Admin/AdminProductCategoris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductsId,CatId,ProductCatId")] ProductCategori productCategori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCategori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName", productCategori.CatId);
            ViewData["ProductsId"] = new SelectList(_context.Products, "ProductId", "ProductName", productCategori.ProductsId);
            return View(productCategori);
        }

        // GET: Admin/AdminProductCategoris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategori = await _context.ProductCategoris.FindAsync(id);
            if (productCategori == null)
            {
                return NotFound();
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", productCategori.CatId);
            ViewData["ProductsId"] = new SelectList(_context.Products, "ProductId", "ProductId", productCategori.ProductsId);
            return View(productCategori);
        }

        // POST: Admin/AdminProductCategoris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductsId,CatId,ProductCatId")] ProductCategori productCategori)
        {
            if (id != productCategori.ProductCatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCategori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoriExists(productCategori.ProductCatId))
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
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", productCategori.CatId);
            ViewData["ProductsId"] = new SelectList(_context.Products, "ProductId", "ProductId", productCategori.ProductsId);
            return View(productCategori);
        }

        // GET: Admin/AdminProductCategoris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategori = await _context.ProductCategoris
                .Include(p => p.Cat)
                .Include(p => p.Products)
                .FirstOrDefaultAsync(m => m.ProductCatId == id);
            if (productCategori == null)
            {
                return NotFound();
            }

            return View(productCategori);
        }

        // POST: Admin/AdminProductCategoris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productCategori = await _context.ProductCategoris.FindAsync(id);
            _context.ProductCategoris.Remove(productCategori);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoriExists(int id)
        {
            return _context.ProductCategoris.Any(e => e.ProductCatId == id);
        }
    }
}
