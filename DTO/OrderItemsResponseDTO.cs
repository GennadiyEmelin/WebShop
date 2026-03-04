namespace WebShop.DTO
{
    public class OrderItemsResponseDTO
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }
    }
}
