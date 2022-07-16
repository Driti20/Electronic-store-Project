using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Electronics_Store.Areas.ProductArea.Models;
using Electronics_Store.Data;
using Microsoft.AspNetCore.Authorization;
using Electronics_Store.Helpers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections.Generic;

namespace Electronics_Store.Areas.ProductArea.Controllers
{
    [Area("ProductArea")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: ProductArea/Products
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber, string availability, string filteredBy)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (filteredBy != null)
            {
                availability = filteredBy;
                ViewData["filtered_by"] = filteredBy;
            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var products = from p in _context.Products
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(b => b.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            List<Guid> arraying = new List<Guid>();
            List<Guid> arraying2 = new List<Guid>();
            
            if (products.Any())
            {
                foreach (var product in products)
                {
                    var stock = _context.Stocks.Where(s => s.ProductId == product.Id);
                    if (stock.Any())
                    {
                        foreach (var stc in stock)
                        {
                            if (stc.AvailableUnits > 0)
                            {
                                if (arraying.Any())
                                {
                                    if (!arraying.Contains(stc.ProductId))
                                    {
                                        arraying.Add(stc.ProductId);
                                    }
                                }
                                else if (!arraying.Any())
                                {
                                    arraying.Add(product.Id);
                                }
                            }
                            else if (stc.AvailableUnits == 0)
                            {
                                if (arraying2.Any())
                                {
                                    if (!arraying2.Contains(stc.ProductId))
                                    {
                                        arraying2.Add(stc.ProductId);
                                    }
                                }
                                else if (!arraying2.Any())
                                {
                                    arraying2.Add(product.Id);
                                }
                            }
                        }
                    }
                    else if (!stock.Any() && availability != null)
                    {
                        products = products.Where(p => p.Id != product.Id);
                    }
                    stock = null;
                }
                if (availability == "unavailable" && arraying.Any())
                {
                    if (arraying.Count > 0)
                    {
                        for (var i = 0; i < arraying.Count; i++)
                        {
                            var theSpecificProduct = await _context.Products.FindAsync(arraying[i]);
                            if (theSpecificProduct != null)
                            {
                                products = products.Where(a => a.Id != theSpecificProduct.Id);
                            }
                        }
                    }
                }
                else if (availability == "available" && arraying2.Any())
                {
                    if (arraying2.Count > 0)
                    {
                        for (var i = 0; i < arraying2.Count; i++)
                        {
                            if (!arraying.Contains(arraying2[i]))
                            {
                                var theSpecificProduct = await _context.Products.FindAsync(arraying2[i]);
                                if (theSpecificProduct != null)
                                {
                                    products = products.Where(a => a.Id != theSpecificProduct.Id);
                                }
                            }
                        }
                    }
                }
                if (products != null)
                {
                    ViewData["NoResultsFilterAvailability"] = "No products were found with the corresponding filter!";
                }
            }

            if (products.Any())
            {
                int pageSize = 6;
                ViewData["TotalItems"] = products.Count();
                return View(await PaginatedList<Product>.CreateAsync(products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Color).Include(p => p.Material).Include(p => p.Warranty).AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            else
            {
                products = _context.Products;
                if (products.Any())
                {
                    ViewData["TotalItems"] = products.Count();
                    if (ViewData["NoResultsFilterAvailability"] == null)
                    {
                        TempData["NoProductsFoundOnFilter"] = "None of the products contains these letters";
                    }
                    else
                    {
                        TempData["NoProductsFoundOnFilter"] = ViewData["NoResultsFilterAvailability"];
                    }
                }
                int pageSize = 6;
                return View(await PaginatedList<Product>.CreateAsync(products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Color).Include(p => p.Material).Include(p => p.Warranty).AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }

        // GET: ProductArea/Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
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

                foreach (var stock in stocks)
                {
                    count += stock.AvailableUnits;
                }

                ViewData["AvailableInStock"] = count;
            }

            return View(product);
        }

        // GET: ProductArea/Products/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name");
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name");
            ViewData["WarrantyId"] = new SelectList(_context.Warranties, "Id", "Duration");
            return View();
        }

        // POST: ProductArea/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Slug,ImageFile,IsActive,Weight,ColorId,BrandId,MaterialId,WarrantyId,CategoryId,CreatedDate,ModifiedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    //Save image to wwwroot/ImageOfProduct
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                    string extension = Path.GetExtension(product.ImageFile.FileName);
                    product.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/ImageOfProduct/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }

                    product.Id = Guid.NewGuid();
                    product.Slug = product.Name.ToLower().Replace(" ", "-");
                    product.CreatedDate = DateTime.Now;
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["ImageNotNull"] = "You should attach an image";
                    ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
                    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
                    ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name");
                    ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name");
                    ViewData["WarrantyId"] = new SelectList(_context.Warranties, "Id", "Duration");
                    return View(product);
                }
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name", product.ColorId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", product.MaterialId);
            ViewData["WarrantyId"] = new SelectList(_context.Warranties, "Id", "Duration", product.WarrantyId);
            return View(product);
        }

        // GET: ProductArea/Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name", product.ColorId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", product.MaterialId);
            ViewData["WarrantyId"] = new SelectList(_context.Warranties, "Id", "Duration", product.WarrantyId);
            return View(product);
        }

        // POST: ProductArea/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Price,Slug,ImageFile,IsActive,Weight,ColorId,BrandId,MaterialId,WarrantyId,CategoryId,CreatedDate,ModifiedDate")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var theProduct = await _context.Products.FindAsync(id);

                    //Delete image from wwwroot/ImageOfProduct
                    if (product.ImageFile != null && theProduct.Image != null && theProduct.Image != product.ImageFile.ToString())
                    {
                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "ImageOfProduct", theProduct.Image);
                        if (System.IO.File.Exists(imagePath))
                            System.IO.File.Delete(imagePath);
                        
                        //Save image to wwwroot/ImageOfProduct
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                        string extension = Path.GetExtension(product.ImageFile.FileName);
                        theProduct.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/ImageOfProduct/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await product.ImageFile.CopyToAsync(fileStream);
                        }

                        if (theProduct.Name != product.Name)
                        {
                            theProduct.Name = product.Name;
                            theProduct.Slug = theProduct.Name.ToLower().Replace(" ", "-");
                        }
                        theProduct.Price = product.Price;
                        theProduct.Description = product.Description;
                        theProduct.IsActive = product.IsActive;
                        theProduct.Weight = product.Weight;
                        theProduct.ColorId = product.ColorId;
                        theProduct.BrandId = product.BrandId;
                        theProduct.MaterialId = product.MaterialId;
                        theProduct.CategoryId = product.CategoryId;
                        theProduct.WarrantyId = product.WarrantyId;

                        theProduct.ModifiedDate = DateTime.Now;
                        _context.Update(theProduct);
                        await _context.SaveChangesAsync();
                    }
                    else if (theProduct.Image == null && product.ImageFile != null)
                    {
                        //Save image to wwwroot/ImageOfProduct
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                        string extension = Path.GetExtension(product.ImageFile.FileName);
                        theProduct.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/ImageOfProduct/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await product.ImageFile.CopyToAsync(fileStream);
                        }

                        if (theProduct.Name != product.Name)
                        {
                            theProduct.Name = product.Name;
                            theProduct.Slug = theProduct.Name.ToLower().Replace(" ", "-");
                        }
                        theProduct.Price = product.Price;
                        theProduct.Description = product.Description;
                        theProduct.IsActive = product.IsActive;
                        theProduct.Weight = product.Weight;
                        theProduct.ColorId = product.ColorId;
                        theProduct.BrandId = product.BrandId;
                        theProduct.MaterialId = product.MaterialId;
                        theProduct.CategoryId = product.CategoryId;
                        theProduct.WarrantyId = product.WarrantyId;

                        theProduct.ModifiedDate = DateTime.Now;
                        _context.Update(theProduct);
                        await _context.SaveChangesAsync();
                    }
                    else if (product.Image == null)
                    {
                        if (theProduct.Name != product.Name)
                        {
                            theProduct.Name = product.Name;
                            theProduct.Slug = theProduct.Name.ToLower().Replace(" ", "-");
                        }
                        theProduct.Price = product.Price;
                        theProduct.Description = product.Description;
                        theProduct.IsActive = product.IsActive;
                        theProduct.Weight = product.Weight;
                        theProduct.ColorId = product.ColorId;
                        theProduct.BrandId = product.BrandId;
                        theProduct.MaterialId = product.MaterialId;
                        theProduct.CategoryId = product.CategoryId;
                        theProduct.WarrantyId = product.WarrantyId;

                        theProduct.ModifiedDate = DateTime.Now;
                        _context.Update(theProduct);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name", product.ColorId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", product.MaterialId);
            ViewData["WarrantyId"] = new SelectList(_context.Warranties, "Id", "Id", product.WarrantyId);
            return View(product);
        }

        // GET: ProductArea/Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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

            return View(product);
        }

        // POST: ProductArea/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            //Delete image from wwwroot/ImageOfProduct
            if(product.Image != null)
            {
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "ImageOfProduct", product.Image);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }

            //delete the record
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
