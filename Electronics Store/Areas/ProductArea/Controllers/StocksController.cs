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
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductArea/Stocks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Stocks.Include(s => s.Branch).Include(s => s.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductArea/Stocks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.Branch)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // GET: ProductArea/Stocks/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: ProductArea/Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InitialUnits,CostPerUnit,RecommendedSalePricePerUnit,BranchId,ProductId")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                stock.Id = Guid.NewGuid();
                stock.RegistrationDate = DateTime.Now;
                stock.AvailableUnits = stock.InitialUnits;
                _context.Add(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name", stock.BranchId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", stock.ProductId);
            return View(stock);
        }

        // GET: ProductArea/Stocks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name", stock.BranchId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", stock.ProductId);
            return View(stock);
        }

        // POST: ProductArea/Stocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,InitialUnits,AvailableUnits,CostPerUnit,RecommendedSalePricePerUnit,RegistrationDate,BranchId,ProductId")] Stock stock)
        {
            if (id != stock.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockExists(stock.Id))
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
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name", stock.BranchId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", stock.ProductId);
            return View(stock);
        }

        // GET: ProductArea/Stocks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.Branch)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // POST: ProductArea/Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockExists(Guid id)
        {
            return _context.Stocks.Any(e => e.Id == id);
        }
    }
}
