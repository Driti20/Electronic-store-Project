using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Electronics_Store.Areas.ProductArea.Models;
using Electronics_Store.Data;
using Electronics_Store.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Electronics_Store.Areas.ProductArea.Controllers
{
    [Area("ProductArea")]
    [Authorize(Roles = "Admin")]
    public class ColorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ColorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductArea/Colors
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var colors = from c in _context.Colors
                         select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                colors = colors.Where(c => c.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    colors = colors.OrderByDescending(c => c.Name);
                    break;
                default:
                    colors = colors.OrderBy(c => c.Name);
                    break;
            }
            if (colors.Any())
            {
                int pageSize = 3;
                return View(await PaginatedList<Color>.CreateAsync(colors.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            else
            {
                colors = _context.Colors;
                if (colors.Any())
                {
                    TempData["NoColorsFoundOnFilter"] = "None of the colors contains these letters";
                }
                int pageSize = 3;
                return View(await PaginatedList<Color>.CreateAsync(colors.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }

        // GET: ProductArea/Colors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.Colors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: ProductArea/Colors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductArea/Colors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedDate,ModifiedDate")] Color color)
        {
            if (ModelState.IsValid)
            {
                color.Id = Guid.NewGuid();
                color.CreatedDate = DateTime.Now;
                _context.Add(color);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        // GET: ProductArea/Colors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.Colors.FindAsync(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        // POST: ProductArea/Colors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedDate,ModifiedDate")] Color color)
        {
            if (id != color.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    color.ModifiedDate = DateTime.Now;
                    _context.Update(color);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorExists(color.Id))
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
            return View(color);
        }

        // GET: ProductArea/Colors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.Colors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // POST: ProductArea/Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var color = await _context.Colors.FindAsync(id);
            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColorExists(Guid id)
        {
            return _context.Colors.Any(e => e.Id == id);
        }
    }
}
