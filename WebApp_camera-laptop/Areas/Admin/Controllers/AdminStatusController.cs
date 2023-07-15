﻿using System;
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
    public class AdminStatusController : Controller
    {
        private readonly webap_camera_laptopContext _context;

        public AdminStatusController(webap_camera_laptopContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminStatus
        public async Task<IActionResult> Index()
        {
            try {
            return View(await _context.Statuses.ToListAsync());
            }
            catch (Exception ex) { return View("Error"); }
        }

        // GET: Admin/AdminStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try {
                if (id == null)
                {
                    return NotFound();
                }

                var status = await _context.Statuses
                    .FirstOrDefaultAsync(m => m.StatusId == id);
                if (status == null)
                {
                    return NotFound();
                }

                return View(status);
            } catch (Exception ex) { return View("Error"); }
           
        }

        // GET: Admin/AdminStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusId,StatusName,Description")] Status status)
        {
            if (ModelState.IsValid)
            {
                _context.Add(status);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        // GET: Admin/AdminStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try {
                if (id == null)
                {
                    return NotFound();
                }

                var status = await _context.Statuses.FindAsync(id);
                if (status == null)
                {
                    return NotFound();
                }
                return View(status);
            } catch (Exception ex) { return View("Error"); }
           
        }

        // POST: Admin/AdminStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusId,StatusName,Description")] Status status)
        {
            if (id != status.StatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(status);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusExists(status.StatusId))
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
            return View(status);
        }

        // GET: Admin/AdminStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: Admin/AdminStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusExists(int id)
        {
            return _context.Statuses.Any(e => e.StatusId == id);
        }
    }
}
