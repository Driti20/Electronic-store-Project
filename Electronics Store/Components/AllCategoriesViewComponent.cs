using Electronics_Store.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics_Store.Components
{
    public class AllCategoriesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public AllCategoriesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid mainCategory)
        {
            var categories = _context.Categories.Where(c => c.Main_CategoryId == mainCategory);
            return View(categories);
        }
    }
}
