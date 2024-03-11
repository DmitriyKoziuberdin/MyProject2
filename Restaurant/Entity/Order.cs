namespace Restaurant.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderName { get; set; } = null!;
        public decimal Price { get; set; }
        public List<OrderHistory> OrderHistories { get; set; }
    }
}
