using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StackBuldTechnicalAssessment.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }

        [Precision(18, 2)]
        public decimal SubTotal => Quantity * UnitPrice;

        public Order Order { get; set; } = null!;

        public Product Product { get; set; } = null!;
    }
}
