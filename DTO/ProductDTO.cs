using System.ComponentModel.DataAnnotations;

namespace WebShop.DTO
{
    public class ProductDTO
    {
        [Required(ErrorMessage = "Введите имя товара!")]
        [MinLength(3, ErrorMessage = "Название должно быть не короче 3 букв")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите описание товара!")]
        [MinLength(6, ErrorMessage = "Описание должно быть не короче 6 букв")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите цену товара!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0!")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Введите количество товара!")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество товара не может быть отрицательным!")]
        public int StockQuantity { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
    }
}
