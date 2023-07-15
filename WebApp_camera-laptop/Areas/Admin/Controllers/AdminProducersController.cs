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
    public class AdminProducersController : Controller
    {
        private readonly webap_camera_laptopContext _context;

        public AdminProducersController(webap_camera_laptopContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminProducers
        public async Task<IActionResult> Index()
        {
            try {

            return View(await _context.Producers.ToListAsync());

            }
            catch (Exception ex) { return View("Error"); }
        }

        // GET: Admin/AdminProducers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try {
                if (id == null)
                {
                    return NotFound();
                }

                var producer = await _context.Producers
                    .FirstOrDefaultAsync(m => m.ProducerId == id);
                if (producer == null)
                {
                    return NotFound();
                }

                return View(producer);
            } catch (Exception) { return View("Error"); }
           
        }

        // GET: Admin/AdminProducers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminProducers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProducerId,ProducerName,Description,Thumb")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Admin/AdminProducers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try {
                if (id == null)
                {
                    return NotFound();
                }

                var producer = await _context.Producers.FindAsync(id);
                if (producer == null)
                {
                    return NotFound();
                }
                return View(producer);
            } catch (Exception ex) { return View("Error"); }
           
        }

        // POST: Admin/AdminProducers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProducerId,ProducerName,Description,Thumb")] Producer producer)
        {
            if (id != producer.ProducerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducerExists(producer.ProducerId))
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
            return View(producer);
        }

        // GET: Admin/AdminProducers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = await _context.Producers
                .FirstOrDefaultAsync(m => m.ProducerId == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // POST: Admin/AdminProducers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = await _context.Producers.FindAsync(id);
            _context.Producers.Remove(producer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducerExists(int id)
        {
            return _context.Producers.Any(e => e.ProducerId == id);
        }
    }
}
