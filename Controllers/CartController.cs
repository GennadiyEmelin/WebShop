using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.DTO;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartServices;
        private readonly IProductServices _productServices;
        public CartController(ICartServices cartServices, IProductServices productServices)
        {
            _productServices = productServices;
            _cartServices = cartServices;
        }

        [HttpPost("AddToCart/{productId}")]
        public async Task<IActionResult> AddToCart(Guid productId, string UserId)
        {
            try
            {
                var product = await _productServices.GetProductById(productId);
                if (product == null)
                {
                    return NotFound("Продукт не найден.");
                }
                await _cartServices.Add(product, UserId);
                return Ok("Продукт добавлен в корзину.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при добавлении продукта в корзину.");
            }
        }

        [HttpGet("GetCart/{UserId}")]
        public async Task<ActionResult<CartResponseDTO>> GetCart(string UserId)
        {
            try
            {
                var cart = await _cartServices.TryGetByUserId(UserId);
                if (cart == null)
                {
                    return NotFound("Корзина не найдена.");
                }
                return Ok(_cartServices.TryGetByUserIdResponse(cart));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при получении корзины.");
            }
        }

        [HttpPost("SubstractFromCart/{productId}")]
        public async Task<IActionResult> SubstractFromCart(Guid productId, string UserId)
        {
            try
            {
                await _cartServices.Substract(productId, UserId);
                return Ok("Продукт удален из корзины.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при удалении продукта из корзины.");
            }
        }

        [HttpDelete("ClearCart/{UserId}")]
        public async Task<IActionResult> DeleteCart(string UserId)
        {
            try
            {
                var cart = await _cartServices.TryGetByUserId(UserId);
                if (cart == null)
                {
                    return NotFound("Корзина не найдена.");
                }
                await _cartServices.Clear(UserId);
                return Ok("Корзина очищена");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при очищении корзины.");
            }
        }
    }
}
