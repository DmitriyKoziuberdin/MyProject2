namespace Restaurant.DTO.Outcoming
{
    public class OrderCountDto
    {
        public int TotalClientCount { get; set; }
        public List<string>? ClientEmails { get; set; }
    }
}
