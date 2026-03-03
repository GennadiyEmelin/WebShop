using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.DTO;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct([FromBody]ProductDTO dto)
        {
            if(!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage))
                    .ToList();

                return BadRequest($"Ошибки: {string.Join(", ", errors)}");
            }
            try
            {
                var product = new Product
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    StockQuantity = dto.StockQuantity,
                    IsActive = dto.IsActive,
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _productServices.AddProduct(product);
                return Ok(product);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при создании продукта.");
            }
        }
    }
}
