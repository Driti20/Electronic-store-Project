using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Electronics_Store.Areas.Client.Models;
using Electronics_Store.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Electronics_Store.Areas.Client.ViewModels;
using Electronics_Store.Helpers;

namespace Electronics_Store.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = "Admin")]
    public class SlideShowsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SlideShowsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Client/SlideShows
        public async Task<IActionResult> Index(string status, string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            if (status != null)
            {
                if (status == "Active")
                {
                    ViewData["status"] = status;
                    var slideShows1 = from s in _context.SlideShows
                                     select s;
                    if (searchString != null)
                    {
                        pageNumber = 1;
                    }
                    else
                    {
                        searchString = currentFilter;
                    }
                    int pageSize1 = 3;
                    return View(await PaginatedList<SlideShow>.CreateAsync(slideShows1.Where(s => s.Status == true).AsNoTracking(), pageNumber ?? 1, pageSize1));
                }
                else if (status == "InActive")
                {
                    ViewData["status"] = status;
                    var slideShows2 = from s in _context.SlideShows
                                     select s;
                    if (searchString != null)
                    {
                        pageNumber = 1;
                    }
                    else
                    {
                        searchString = currentFilter;
                    }
                    int pageSize2 = 3;
                    return View(await PaginatedList<SlideShow>.CreateAsync(slideShows2.Where(s => s.Status == false).AsNoTracking(), pageNumber ?? 1, pageSize2));
                }
            }

            var slideShows = from s in _context.SlideShows
                           select s;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            int pageSize = 3;
            return View(await PaginatedList<SlideShow>.CreateAsync(slideShows.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Client/SlideShows/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slideShow = await _context.SlideShows
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slideShow == null)
            {
                return NotFound();
            }

            return View(slideShow);
        }

        // GET: Client/SlideShows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/SlideShows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SlideShowViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string uniqueFileName = null;
                    if (model.Image != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "slideshow_images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    }

                    SlideShow slideShow = new SlideShow
                    {
                        Name = uniqueFileName,
                        Status = model.Status
                    };

                    slideShow.Id = Guid.NewGuid();
                    _context.Add(slideShow);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["ImageNotNull"] = "You should attach an image";
                    return View(model);
                }
            }
            return View(model);
        }

        // GET: Client/SlideShows/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var slide = await _context.SlideShows.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }

            var model = new SlideShowViewModel
            {
                Id = slide.Id,
                Status = slide.Status
            };

            var slideShow = await _context.SlideShows.FindAsync(id);
            if (slideShow == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Client/SlideShows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Image,Status")] SlideShowViewModel slideShow)
        {
            if (id != slideShow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(slideShow);
                    //await _context.SaveChangesAsync();

                    var theSlideshow = await _context.SlideShows.FindAsync(id);

                    //Delete image from wwwroot/slideshow_images
                    if (slideShow.Image != null && theSlideshow.Name != null && theSlideshow.Name != slideShow.Image.ToString())
                    {
                        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "slideshow_images", theSlideshow.Name);
                        if (System.IO.File.Exists(imagePath))
                            System.IO.File.Delete(imagePath);

                        //Save image to wwwroot/slideshow_images
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(slideShow.Image.FileName);
                        string extension = Path.GetExtension(slideShow.Image.FileName);
                        theSlideshow.Name = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/slideshow_images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await slideShow.Image.CopyToAsync(fileStream);
                        }

                        theSlideshow.Status = slideShow.Status;

                        _context.Update(theSlideshow);
                        await _context.SaveChangesAsync();
                    }
                    else if (theSlideshow.Name == null && slideShow.Image != null)
                    {
                        //Save image to wwwroot/slideshow_images
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(slideShow.Image.FileName);
                        string extension = Path.GetExtension(slideShow.Image.FileName);
                        theSlideshow.Name = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/slideshow_images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await slideShow.Image.CopyToAsync(fileStream);
                        }

                        theSlideshow.Status = slideShow.Status;

                        _context.Update(theSlideshow);
                        await _context.SaveChangesAsync();
                    }
                    else if (slideShow.Image == null)
                    {
                        theSlideshow.Status = slideShow.Status;

                        _context.Update(theSlideshow);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlideShowExists(slideShow.Id))
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
            return View(slideShow);
        }

        // GET: Client/SlideShows/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slideShow = await _context.SlideShows
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slideShow == null)
            {
                return NotFound();
            }

            return View(slideShow);
        }

        // POST: Client/SlideShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var slideShow = await _context.SlideShows.FindAsync(id);

            //Delete image from wwwroot/slideshow_images
            if (slideShow.Name != null)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "slideshow_images", slideShow.Name);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }

            _context.SlideShows.Remove(slideShow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SlideShowExists(Guid id)
        {
            return _context.SlideShows.Any(e => e.Id == id);
        }
    }
}
