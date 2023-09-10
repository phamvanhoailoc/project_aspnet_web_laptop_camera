using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using WebApp_camera_laptop.Helpper;
using WebApp_camera_laptop.Models;
using WebApp_camera_laptop.Areas.Admin.ModelViews;
using Microsoft.CodeAnalysis;

namespace WebApp_camera_laptop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly webap_camera_laptopContext _context;
        public INotyfService _notyfService { get; }

        public AdminProductsController(webap_camera_laptopContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
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
        public async Task<IActionResult> Create(ProductsAdminViewModel product, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            try
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
                            string image = baseImageName + "_" + imageIndex.ToString() + extension;

                            string uploadedFileName = await Utilities.UploadFile(file, @"products", image);
                            uploadedFileNames.Add(uploadedFileName);
                            imageIndex++;
                        }
                        string thumbString = string.Join(",", uploadedFileNames);
                        product.Thumb = thumbString;
                    }
                    product.Alias = Utilities.SEOUrl(product.ProductName);
                    product.DateModified = DateTime.Now;
                    product.DateCreated = DateTime.Now;

                    Product products = new Product
                    {
                        ProductName = product.ProductName,
                        Thumb = product.Thumb,
                        Alias = product.Alias,
                        DateCreated = product.DateCreated,
                        DateModified = product.DateModified,
                        ShortDesc = product.ShortDesc,
                        Description = product.Description,
                        Price = product.Price,
                        Discount = product.Discount,
                        BestSellers = product.BestSellers,
                        HomeFlag = product.HomeFlag,
                        Active = product.Active,
                        Tags = product.Tags,
                        Title = product.Title,
                        MetaDesc = product.MetaDesc,
                        MetaKey = product.MetaKey,
                        UnitslnStock = product.UnitslnStock,
                        ProducerId = product.ProducerId,
                        StatusId = product.StatusId,
                        Cpu = product.Cpu,
                        Ram = product.Ram,
                        HardDrive = product.HardDrive,
                        GraphicCard = product.GraphicCard,
                        Screen = product.Screen,
                        Location = product.Location,
                        TypeCameraId = product.TypeCameraId,
                        Resoluton = product.Resoluton,
                        CameraAngle = product.CameraAngle,
                        Storege = product.Storege,
                        FarInfraredVision = product.FarInfraredVision,
                        DeviceSupport = product.DeviceSupport,
                        ControlPhone = product.ControlPhone,
                        Utilities = product.Utilities,
                        Conversation = product.Conversation,
                        Adapter = product.Adapter,
                        InputPower = product.InputPower,
                        Size = product.Size,
                    };
                    _context.Add(products);
                    await _context.SaveChangesAsync();

                    // Sử dụng DbContext.Entry để lấy đối tượng sau khi đã thêm vào cơ sở dữ liệu
                    var addedProduct = _context.Entry(products).Entity;

                    // Bây giờ bạn có thể truy cập ProductId
                    int productId = addedProduct.ProductId;


                    if (product.ProductCategories != null)
                    {
                        // Tạo danh sách các đối tượng ProductCategori từ dữ liệu trong model
                        foreach (var categoryId in product.ProductCategories)
                        {
                                var productCategory = new ProductCategori
                                {
                                    ProductsId = productId, // Sử dụng ProductId từ sản phẩm đã lưu
                                    CatId = categoryId
                                };
                                _context.Add(productCategory);
                        }
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", product.CatId);
                ViewData["ProducerId"] = new SelectList(_context.Producers, "ProducerId", "ProducerId", product.ProducerId);
                ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", product.StatusId);
                return View(product);
            }
            catch (Exception ex) {
                return View("Error");
            }
           
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
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.Active = false;
                _context.Update(product);
                _notyfService.Success("Xóa thành công");
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null) {
                product.Active = false;
                _context.Update(product);
                _notyfService.Success("Xóa thành công");
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
