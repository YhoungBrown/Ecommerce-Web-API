using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StackBuldTechnicalAssessment.Models
{
    public class ProductDto
    {
        [MaxLength(100), Required]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000), Required]
        public string Description { get; set; } = string.Empty;

        [Precision(18, 2), Required]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }
    }
}