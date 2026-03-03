using WebShop.Data;
using WebShop.Models;

namespace WebShop.Services
{
    public class ProductServices : IProductServices
    {
        private readonly AppDbContext _context;
        public ProductServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddProduct(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch
            {

            }
        }

        public Task DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = new List<Product>();
            await foreach (var product in _context.Products)
            {
                if(product.IsActive == false) continue;
                products.Add(product);
            }
            return products;
        }

        public Task<Product> GetProductById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
