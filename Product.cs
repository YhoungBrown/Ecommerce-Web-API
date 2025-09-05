using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StackBuldTechnicalAssessment.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Product
    {
        public int Id { get; set; }

       [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Precision(18,2)]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Timestamp]
        public byte[] ConcurrencyToken { get; set; } = Array.Empty<byte>();
    }
}