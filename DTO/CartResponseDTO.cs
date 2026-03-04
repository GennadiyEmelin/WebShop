namespace WebShop.DTO
{
    public class CartResponseDTO
    {
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartItemsResponseDTO> Items { get; set; }
    }
}
