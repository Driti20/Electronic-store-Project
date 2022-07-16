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
    public class BranchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BranchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductArea/Branches
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

            var branches = from b in _context.Branches
                        select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                branches = branches.Where(b => b.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    branches = branches.OrderByDescending(b => b.Name);
                    break;
                default:
                    branches = branches.OrderBy(b => b.Name);
                    break;
            }

            if (branches.Any())
            {
                int pageSize = 3;
                return View(await PaginatedList<Branch>.CreateAsync(branches.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            else
            {
                branches = _context.Branches;
                if (branches.Any())
                {
                    TempData["NoBranchesFoundOnFilter"] = "None of the branches contains these letters";
                }
                int pageSize = 3;
                return View(await PaginatedList<Branch>.CreateAsync(branches.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }

        // GET: ProductArea/Branches/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // GET: ProductArea/Branches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductArea/Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedDate,ModifiedDate")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                branch.Id = Guid.NewGuid();
                branch.CreatedDate = DateTime.Now;
                _context.Add(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

        // GET: ProductArea/Branches/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            return View(branch);
        }

        // POST: ProductArea/Branches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedDate,ModifiedDate")] Branch branch)
        {
            if (id != branch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    branch.ModifiedDate = DateTime.Now;
                    _context.Update(branch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchExists(branch.Id))
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
            return View(branch);
        }

        // GET: ProductArea/Branches/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // POST: ProductArea/Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var branch = await _context.Branches.FindAsync(id);
            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BranchExists(Guid id)
        {
            return _context.Branches.Any(e => e.Id == id);
        }
    }
}
