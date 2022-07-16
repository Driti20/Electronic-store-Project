using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics_Store.Areas.ProductArea.Models
{
    public class Color
    {
        public Guid Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        [RegularExpression(@"^[a-zA-Z-\s]+$", ErrorMessage = "Only letters are allowed")]
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public List<Product> Products { get; set; }
    }
}
