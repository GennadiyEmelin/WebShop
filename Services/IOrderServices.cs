using WebShop.DTO;
using WebShop.Models;

namespace WebShop.Services
{
    public interface IOrderServices
    {
        Task Add(OrderDTO order);
        Task<List<OrderResponseDTO>> GetAll();
        Task<OrderResponseDTO> GetById(Guid id);
        Task Delete(Guid OrderId);
        Task Update(Guid OrderId, OrderDTO order);
    }
}
