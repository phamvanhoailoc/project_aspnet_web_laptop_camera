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
                    if (fThumb != null )
                    {
                        List<IFormFile> files = Request.Form.Files.ToList();
                        if (files != null && files.Count() > 1)
                        {
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
                        else {
                            _notyfService.Error("Phải thêm tối thiểu 2 ảnh !!!");
                            return View(product);
                        }
                        
                    }
                    else
                    {
                        _notyfService.Error("Phải thêm tối thiểu 2 ảnh !!!");
                        return View(product);
                    }
                    product.Alias = Utilities.SEOUrl(product.ProductName);
                    product.DateModified = DateTime.Now;
                    product.DateCreated = DateTime.Now;

                    if (product.Discount == null) {
                        product.Discount = product.Price;
                    }
                    if (product.Price == null && product.Discount !=null)
                    {
                        product.Price = product.Discount;
                    }

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
                        Title = product.ProductName,
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
                        Configuration = product.Configuration
                    };
                    _context.Add(products);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Tạo sản phẩm thành công");

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
                var danhmucList = await _context.ProductCategoris.Where(pc => pc.ProductsId == id).ToListAsync();
                var catIds = danhmucList.Select(dm => dm.CatId).Distinct().ToList(); // Lấy danh sách các CatId duy nhất từ danhmucList

                ViewData["CatId"] = new MultiSelectList(_context.Categories, "CatId", "CatName", catIds);
                ViewData["ProducerId"] = new SelectList(_context.Producers, "ProducerId", "ProducerName", product.ProducerId);
                ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusName", product.StatusId);
                return View(new ProductsAdminViewModel
                {
                    // Sao chép dữ liệu từ đối tượng sản phẩm vào ProductsAdminViewModel
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Thumb = product.Thumb,
                    Alias = product.Alias,
                    DateCreated = product.DateCreated,
                    DateModified = product.DateModified,
                    ShortDesc = product.ShortDesc,
                    Description = product.Description,
                    Price = product.Price,
                    Discount = product.Discount,
                    BestSellers = (bool)product.BestSellers,
                    HomeFlag = (bool)product.HomeFlag,
                    Active = (bool)product.Active,
                    Tags = product.Tags,
                    Title = product.ProductName,
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
                    Conversation = (bool)product.Conversation,
                    Adapter = product.Adapter,
                    InputPower = product.InputPower,
                    Size = product.Size,
                    Configuration = product.Configuration
                });
            } catch (Exception ex) { return View("Error"); }
            
        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductsAdminViewModel product, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    // Lấy sản phẩm từ cơ sở dữ liệu dựa trên ProductId
                    var existingProduct = await _context.Products.FindAsync(product.ProductId);

                    if (existingProduct == null)
                    {
                        return NotFound();
                    }
                    product.Alias = Utilities.SEOUrl(product.ProductName);

                    if (product.Discount == null)
                    {
                        product.Discount = product.Price;
                    }
                    if (product.Price == null && product.Discount != null)
                    {
                        product.Price = product.Discount;
                    }

                    // Cập nhật thông tin sản phẩm từ ProductsAdminViewModel
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.Alias = Utilities.SEOUrl(product.ProductName);
                    existingProduct.DateCreated = product.DateCreated;
                    existingProduct.DateModified = DateTime.Now;
                    existingProduct.ShortDesc = product.ShortDesc;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price ?? product.Discount;
                    existingProduct.Discount = product.Discount ?? product.Price;
                    existingProduct.BestSellers = product.BestSellers;
                    existingProduct.HomeFlag = product.HomeFlag;
                    existingProduct.Active = product.Active;
                    existingProduct.Tags = product.Tags;
                    existingProduct.Title = product.ProductName;
                    existingProduct.MetaDesc = product.MetaDesc;
                    existingProduct.MetaKey = product.MetaKey;
                    existingProduct.UnitslnStock = product.UnitslnStock;
                    existingProduct.ProducerId = product.ProducerId;
                    existingProduct.StatusId = product.StatusId;
                    existingProduct.Cpu = product.Cpu;
                    existingProduct.Ram = product.Ram;
                    existingProduct.HardDrive = product.HardDrive;
                    existingProduct.GraphicCard = product.GraphicCard;
                    existingProduct.Screen = product.Screen;
                    existingProduct.Location = product.Location;
                    existingProduct.TypeCameraId = product.TypeCameraId;
                    existingProduct.Resoluton = product.Resoluton;
                    existingProduct.CameraAngle = product.CameraAngle;
                    existingProduct.Storege = product.Storege;
                    existingProduct.FarInfraredVision = product.FarInfraredVision;
                    existingProduct.DeviceSupport = product.DeviceSupport;
                    existingProduct.ControlPhone = product.ControlPhone;
                    existingProduct.Utilities = product.Utilities;
                    existingProduct.Conversation = product.Conversation;
                    existingProduct.Adapter = product.Adapter;
                    existingProduct.InputPower = product.InputPower;
                    existingProduct.Size = product.Size;
                    existingProduct.Configuration = product.Configuration;
                    _context.Update(existingProduct);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Cập nhật sản phẩm thành công");

                    // Sử dụng DbContext.Entry để lấy đối tượng sau khi đã thêm vào cơ sở dữ liệu
                    var addedProduct = _context.Entry(existingProduct).Entity;

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
                ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName", product.CatId);
                ViewData["ProducerId"] = new SelectList(_context.Producers, "ProducerId", "ProducerName", product.ProducerId);
                ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusName", product.StatusId);
                return View(product);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
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
