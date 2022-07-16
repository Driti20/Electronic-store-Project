using Electronics_Store.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics_Store.Components
{
    public class AllMainCategoriesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public AllMainCategoriesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _context.MainCategories;
            return View(categories);
        }
    }
}
