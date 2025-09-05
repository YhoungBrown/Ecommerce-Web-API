using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace StackBuldTechnicalAssessment.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string CustomerName { get; set; } = null!;

        [Required, MaxLength(150), EmailAddress]
        public string CustomerEmail { get; set; } = null!;

        [Required, MaxLength(500)]
        public string CustomerAddress { get; set; } = null!;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public int OrderStatusId { get; set; } = 1;

        public List <OrderItem> OrderItems { get; set; } = new();

        public OrderStatus OrderStatus { get; set; } = null;

        public bool IsPaid { get; set; } = false;

        [Timestamp]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    }
}