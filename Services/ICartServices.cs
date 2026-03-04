using WebShop.DTO;
using WebShop.Models;

namespace WebShop.Services
{
    public interface ICartServices
    {
        Task<Cart> TryGetByUserId(string UserId);
        CartResponseDTO TryGetByUserIdResponse(Cart cart);
        Task Add(Product product, string UserId);
        Task Substract(Guid productId, string UserId);
        Task Clear(string UserId);
    }
}
