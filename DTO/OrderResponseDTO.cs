using WebShop.Enums;
using WebShop.Models;

namespace WebShop.DTO
{
    public class OrderResponseDTO
    {
        public string UserId { get; set; }
        public string Address { get; set; }
        public decimal TotalAmount { get; set; }
        public Status Status { get; set; }
        public List<OrderItemsResponseDTO> Items { get; set; }

    }
}
