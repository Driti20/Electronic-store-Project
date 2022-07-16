using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics_Store.Areas.Client.ViewModels
{
    public class SlideShowViewModel
    {
        public Guid Id { get; set; }
        public IFormFile Image { get; set; }
        public bool Status { get; set; }
    }
}
