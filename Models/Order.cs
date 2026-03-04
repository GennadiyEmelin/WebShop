using System.ComponentModel.DataAnnotations;
using WebShop.DTO;
using WebShop.Enums;

namespace WebShop.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public Status Status { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
