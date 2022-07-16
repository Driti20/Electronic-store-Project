using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Electronics_Store.Areas.ProductArea.Models;
using Electronics_Store.Data;

namespace Electronics_Store.Areas.ProductArea.Controllers
{
    [Area("ProductArea")]
    public class Main_CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Main_CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductArea/Main_Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.MainCategories.ToListAsync());
        }

        // GET: ProductArea/Main_Categories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var main_Category = await _context.MainCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (main_Category == null)
            {
                return NotFound();
            }

            return View(main_Category);
        }

        // GET: ProductArea/Main_Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductArea/Main_Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Main_Category main_Category)
        {
            if (ModelState.IsValid)
            {
                main_Category.Id = Guid.NewGuid();
                _context.Add(main_Category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(main_Category);
        }

        // GET: ProductArea/Main_Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var main_Category = await _context.MainCategories.FindAsync(id);
            if (main_Category == null)
            {
                return NotFound();
            }
            return View(main_Category);
        }

        // POST: ProductArea/Main_Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description")] Main_Category main_Category)
        {
            if (id != main_Category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(main_Category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Main_CategoryExists(main_Category.Id))
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
            return View(main_Category);
        }

        // GET: ProductArea/Main_Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var main_Category = await _context.MainCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (main_Category == null)
            {
                return NotFound();
            }

            return View(main_Category);
        }

        // POST: ProductArea/Main_Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var main_Category = await _context.MainCategories.FindAsync(id);
            _context.MainCategories.Remove(main_Category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Main_CategoryExists(Guid id)
        {
            return _context.MainCategories.Any(e => e.Id == id);
        }
    }
}
