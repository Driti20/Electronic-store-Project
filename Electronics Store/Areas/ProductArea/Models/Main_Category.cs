using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics_Store.Areas.ProductArea.Models
{
    public class Main_Category
    {
        public Guid Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Category> Categories { get; set; }
    }
}
