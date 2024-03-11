namespace Restaurant.DTO.Outcoming
{
    public class OrderHistoryDto
    {
        public int Id { get; set; }
        public string OrderName { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
