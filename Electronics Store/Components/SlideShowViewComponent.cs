using Electronics_Store.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics_Store.Components
{
    public class SlideShowViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SlideShowViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var slideshowImages = _context.SlideShows.Where(s => s.Status == true).Take(3);
            return View(slideshowImages);
        }
    }
}
