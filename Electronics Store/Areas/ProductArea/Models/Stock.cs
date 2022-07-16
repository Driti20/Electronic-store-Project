using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics_Store.Areas.ProductArea.Models
{
    public class Stock
    {
        public Guid Id { get; set; }

        [Required, Range(1, 50000)]
        [DisplayName("Initial Units")]
        public int InitialUnits { get; set; }

        [DisplayName("Available Units")]
        public int AvailableUnits { get; set; }

        [DisplayName("Cost per unit")]
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPerUnit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayName("Recommended Sale Price per Unit")]
        public decimal RecommendedSalePricePerUnit { get; set; }

        [DisplayName("Registration Date")]
        public DateTime? RegistrationDate { get; set; }

        [DisplayName("Branch")]
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }

        [DisplayName("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
