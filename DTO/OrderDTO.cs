using System.ComponentModel.DataAnnotations;

namespace WebShop.DTO
{
    public class OrderDTO
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Введите адрес доставки!")]
        public string Address { get; set; }
    }
}
