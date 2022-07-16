using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics_Store.Areas.ProductArea.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required, Range(1, 50000)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Slug { get; set; }

        [DisplayName("Image Name")]
        public string Image { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }  
        public bool IsActive { get; set; }
        public int Weight { get; set; }

        [DisplayName("Color")]
        public Guid ColorId { get; set; }
        public Color Color { get; set; }

        [DisplayName("Brand")]
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }

        [DisplayName("Material")]
        public Guid MaterialId { get; set; }
        public Material Material { get; set; }

        [DisplayName("Warranty")]
        public Guid WarrantyId { get; set; }
        public Warranty Warranty { get; set; }

        [DisplayName("Category")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        [DisplayName("Created Date")]
        public DateTime? CreatedDate { get; set; }

        [DisplayName("Modified Date")]
        public DateTime? ModifiedDate { get; set; }


        public List<Stock> Stocks { get; set; }
    }
}
