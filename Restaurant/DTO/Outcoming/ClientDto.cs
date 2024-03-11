namespace Restaurant.DTO.Outcoming
{
    public class ClientDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public List<OrderHistoryDto> OrderHistories { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
