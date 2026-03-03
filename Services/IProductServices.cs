using WebShop.DTO;
using WebShop.Models;

namespace WebShop.Services
{
    public interface IProductServices
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid id);
        Task AddProduct(ProductDTO product);
        Task UpdateProduct(Guid id,ProductDTO product);
        Task DeleteProduct(Guid id); 
    }
}
