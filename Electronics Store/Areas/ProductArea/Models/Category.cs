using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics_Store.Areas.ProductArea.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string Name { get; set; }
        public string Description { get; set; }

        [DisplayName("Created Date")]
        public DateTime? CreatedDate { get; set; }

        [DisplayName("Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        [DisplayName("Main Category")]
        public Guid Main_CategoryId { get; set; }
        public Main_Category Main_Category { get; set; }

        public List<Product> Products { get; set; }
    }
}
