using WebShop.Data;
using WebShop.DTO;
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
        public async Task AddProduct(ProductDTO dto)
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
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch
            {
            }
        }

        public async Task DeleteProduct(Guid id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            product.IsActive = false;
            product.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
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

        public async Task<Product> GetProductById(Guid id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            return product;
        }

        public async Task UpdateProduct(Guid id, ProductDTO product)
        {
            try
            {
                var existingProduct = _context.Products.FirstOrDefault(p => p.Id == id);
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.IsActive = product.IsActive;
                existingProduct.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            } 
            catch { }
        }
    }
}
