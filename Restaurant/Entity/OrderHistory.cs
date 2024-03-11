namespace Restaurant.Entity
{
    public class OrderHistory
    {
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}
