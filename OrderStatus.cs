using System.ComponentModel.DataAnnotations;

namespace StackBuldTechnicalAssessment.Models
{
    public class OrderStatus
    { 
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string StatusName { get; set; } = string.Empty;
    }
}