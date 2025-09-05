namespace StackBuldTechnicalAssessment.Dtos
{
    public class CreateOrderParameterDto
    {
        public string customerName { get; set; } = string.Empty;
        public string customerEmail { get; set; } = string.Empty;
        public string customerAddress { get; set; } = string.Empty;
        public List<CreateOrderItemRequestDto> OrderItems { get; set; } = new();
    }

}