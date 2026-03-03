using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
