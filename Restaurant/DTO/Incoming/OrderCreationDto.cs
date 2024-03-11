namespace Restaurant.DTO.Incoming
{
    public class OrderCreationDto
    {
        public string OrderName { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
