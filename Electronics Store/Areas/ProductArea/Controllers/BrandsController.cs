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
    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductArea/Brands
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
            var brands = from b in _context.Brands
                           select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                brands = brands.Where(b => b.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    brands = brands.OrderByDescending(b => b.Name);
                    break;
                default:
                    brands = brands.OrderBy(b => b.Name);
                    break;
            }
            if (brands.Any())
            {
                int pageSize = 3;
                return View(await PaginatedList<Brand>.CreateAsync(brands.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            else
            {
                brands = _context.Brands;
                if (brands.Any())
                {
                    TempData["NoBrandsFoundOnFilter"] = "None of the brands contains these letters";
                }
                int pageSize = 3;
                return View(await PaginatedList<Brand>.CreateAsync(brands.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }

        // GET: ProductArea/Brands/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: ProductArea/Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductArea/Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedDate,ModifiedDate")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                brand.Id = Guid.NewGuid();
                brand.CreatedDate = DateTime.Now;
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: ProductArea/Brands/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: ProductArea/Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedDate,ModifiedDate")] Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    brand.ModifiedDate = DateTime.Now;
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
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
            return View(brand);
        }

        // GET: ProductArea/Brands/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: ProductArea/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var brand = await _context.Brands.FindAsync(id);
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(Guid id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}
