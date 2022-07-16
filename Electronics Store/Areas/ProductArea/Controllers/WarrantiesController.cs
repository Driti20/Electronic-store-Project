using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Electronics_Store.Areas.ProductArea.Models;
using Electronics_Store.Data;
using Microsoft.AspNetCore.Authorization;

namespace Electronics_Store.Areas.ProductArea.Controllers
{
    [Area("ProductArea")]
    [Authorize(Roles = "Admin")]
    public class WarrantiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WarrantiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductArea/Warranties
        public async Task<IActionResult> Index()
        {
            return View(await _context.Warranties.ToListAsync());
        }

        // GET: ProductArea/Warranties/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warranty = await _context.Warranties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warranty == null)
            {
                return NotFound();
            }

            return View(warranty);
        }

        // GET: ProductArea/Warranties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductArea/Warranties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Duration,CreatedDate,ModifiedDate")] Warranty warranty)
        {
            if (ModelState.IsValid)
            {
                warranty.Id = Guid.NewGuid();
                warranty.CreatedDate = DateTime.Now;
                _context.Add(warranty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(warranty);
        }

        // GET: ProductArea/Warranties/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warranty = await _context.Warranties.FindAsync(id);
            if (warranty == null)
            {
                return NotFound();
            }
            return View(warranty);
        }

        // POST: ProductArea/Warranties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Duration,CreatedDate,ModifiedDate")] Warranty warranty)
        {
            if (id != warranty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    warranty.ModifiedDate = DateTime.Now;
                    _context.Update(warranty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarrantyExists(warranty.Id))
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
            return View(warranty);
        }

        // GET: ProductArea/Warranties/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warranty = await _context.Warranties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warranty == null)
            {
                return NotFound();
            }

            return View(warranty);
        }

        // POST: ProductArea/Warranties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var warranty = await _context.Warranties.FindAsync(id);
            _context.Warranties.Remove(warranty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarrantyExists(Guid id)
        {
            return _context.Warranties.Any(e => e.Id == id);
        }
    }
}
