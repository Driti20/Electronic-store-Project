using Electronics_Store.Areas.ProductArea.Models;
using Electronics_Store.Data;
using Electronics_Store.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics_Store.Areas.Client.Controllers
{
    [Area("Client")]
    public class ClientSideController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientSideController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> ProductsFromCategory(Guid Id, string sortOrder, string searchString, string currentFilter, int? pageNumber, string filteredBy)
        {
            ViewData["CurrentSort"] = sortOrder;
            if (filteredBy != null)
            {
                ViewData["filtered_by"] = filteredBy;
            }

            if (searchString != null)
            {
                ViewData["filtered_by"] = searchString;
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var products = from p in _context.Products.Where(p => p.CategoryId == Id)
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString)
                                       || p.Color.Name.Contains(searchString)
                                       || p.Brand.Name.Contains(searchString)
                                       || p.Material.Name.Contains(searchString)
                                       || p.Description.Contains(searchString));

                if (!products.Any())
                {
                    ViewData["NoFilteredProducts"] = "No products found with the following string: \"" + searchString + "\"";
                }
            }

            switch (sortOrder)
            {
                case "Name":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
            var category = await _context.Categories.FindAsync(Id);
            if (category != null)
            {
                ViewData["Category"] = category.Name;
            }
            int pageSize = 20;
            return View(await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Product(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Material)
                .Include(p => p.Warranty)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            if (product != null)
            {
                var stocks = _context.Stocks.Where(s => s.ProductId == id);
                int count = 0;
                ViewData["AvailableInStock"] = "None in stock";

                foreach (var stock in stocks)
                {
                    count += stock.AvailableUnits;
                }

                if (count != 0)
                {
                    ViewData["AvailableInStock"] = "In stock";
                }
            }

            return View(product);
        }
    }
}
