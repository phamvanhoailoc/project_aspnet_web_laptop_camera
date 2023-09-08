using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using AspNetCoreHero.ToastNotification.Abstractions;
using DiChoSaiGon.Extension;
using DiChoSaiGon.Helpper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using WebApp_camera_laptop.Areas.Admin.ModelViews;
using WebApp_camera_laptop.Models;
using WebQLKSORACLE.Areas.ADMIN.ModelViews;

namespace WebApp_camera_laptop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountsController : Controller
    {
        private readonly webap_camera_laptopContext _context;

        public INotyfService _notyfService { get; }

        public AdminAccountsController(webap_camera_laptopContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminAccounts
        public async Task<IActionResult> Index(int? page, String name, String sdt, String email)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var IsNhanviens = _context.Accounts
            .AsNoTracking()
                    .Include(t => t.Role);
            if (!string.IsNullOrEmpty(name))
            {
                IsNhanviens = IsNhanviens
                                .Where(x => x.FullName.ToUpper().Contains(name.ToUpper()))
                                .OrderByDescending(x => x.FullName)
                                .Include(t => t.Role);
            }
            else if (!string.IsNullOrEmpty(sdt))
            {
                IsNhanviens = IsNhanviens
                                .Where(x => x.Phone.ToString().Contains(sdt))
                                .Include(t => t.Role);

            }
            else if (!string.IsNullOrEmpty(email))
            {
                IsNhanviens = IsNhanviens
                               .Where(x => x.Email.ToUpper().Contains(email.ToUpper()))
                               .OrderByDescending(x => x.Email)
                               .Include(t => t.Role);
            }
            PagedList<Account> models = new PagedList<Account>(IsNhanviens, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewData["DanhMuc"] = new SelectList(_context.Roles, "RoleId", "RoleName");

            return View(models);
        }

        // GET: Admin/AdminAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin/AdminAccounts/Create
        public IActionResult Create()
        {
            ViewData["DanhMuc"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View();
        }

        // POST: Admin/AdminAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterAdminVM taikhoan)
        {
            ViewData["DanhMuc"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            try
            {
                if (ModelState.IsValid)
                {

                    string salt = Utilities.GetRandomKey();
                    Account tk = new Account
                    {
                        FullName = taikhoan.TenNv,
                        Email = taikhoan.EmailNv.Trim().ToLower(),
                        Password = (taikhoan.MatkhauNv + salt.Trim()).ToMD5(),
                        RoleId = taikhoan.RoleId,
                        Salt = salt,
                        Active = taikhoan.TrangthaiNv
                    };
                    try
                    {
                        var checkEmail = _context.Accounts
                            .AsNoTracking()
                            .SingleOrDefault(x => x.Email.ToLower() == taikhoan.EmailNv.ToLower());
                        if (checkEmail != null)
                        {
                            _notyfService.Error(taikhoan.EmailNv + "Tài khoản đã được sử dụng");
                            return View(taikhoan);
                        }
                        _context.Add(tk);
                        await _context.SaveChangesAsync();

                        _notyfService.Success("Đăng ký thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    return View(taikhoan);
                }
            }
            catch
            {
                return View(taikhoan);
            }
        }

        // GET: Admin/AdminAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }

        // POST: Admin/AdminAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,Phone,Email,Password,Salt,Active,FullName,RoleId,LastLogin,CreateDate")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }

        // GET: Admin/AdminAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Admin/AdminAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Route("Login", Name = "Login")]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            var taikhoanID = HttpContext.Session.GetString("Id");
            if (taikhoanID != null)
            {
                return RedirectToAction("Index", "AdminNhanviens");
            }

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginAdminViewModel customer, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isEmail = Utilities.IsValidEmail(customer.UserName);
                    if (!isEmail) return View(customer);
                    var taikhoan = _context.Accounts.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == customer.UserName);
                    if (taikhoan == null)
                    {
                        _notyfService.Error("Thông tin đăng nhập chưa chính xác");
                        return RedirectToAction("Login", "AdminNhanviens");
                    }

                    string pass = (customer.Password + taikhoan.Salt.Trim()).ToMD5();
                    if (taikhoan.Password != pass)
                    {
                        _notyfService.Error("Thông tin đăng nhập chưa chính xác");
                        return View(customer);
                    }
                    //kiem tra active
                    if (taikhoan.Active == false)
                    {
                        _notyfService.Error("Đăng nhập thất bại");
                        return RedirectToAction("Login", "AdminNhanviens");
                    }

                    //luu session
                    HttpContext.Session.SetString("Id", taikhoan.AccountId.ToString());
                    var taikhoanID = HttpContext.Session.GetString("Id");
                    //identity
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, taikhoan.FullName),
                        new Claim("Id",taikhoan.AccountId.ToString()),
                        new Claim(ClaimTypes.Role,taikhoan.RoleId.ToString())
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    _notyfService.Success("Đăng nhập thành công");
                    return RedirectToAction("Index", "AdminNhanviens");
                }
            }
            catch
            {
                _notyfService.Error("Đăng nhập thất bại");
                return RedirectToAction("Login", "AdminNhanviens");
            }
            return View(customer);
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
