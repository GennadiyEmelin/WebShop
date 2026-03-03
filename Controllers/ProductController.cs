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
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage))
                    .ToList();

                return BadRequest($"Ошибки: {string.Join(", ", errors)}");
            }
            try
            {
                await _productServices.AddProduct(dto);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при создании продукта.");
            }
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productServices.GetAllProducts();
                return Ok(products);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при получении продуктов.");
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productServices.GetProductById(id);
            if (product == null)
            {
                return BadRequest();
            }
            if (id == Guid.Empty)
            {
                return BadRequest("Некорректный идентификатор продукта.");
            }
            try
            {
                await _productServices.DeleteProduct(id);
                return Ok("Продукт удалён!");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при удалении продукта.");
            }
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage))
                    .ToList();
                return BadRequest($"Ошибки: {string.Join(", ", errors)}");
            }
            try
            {
                await _productServices.UpdateProduct(id, dto);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при обновлении продукта.");
            }
        }
    }
}