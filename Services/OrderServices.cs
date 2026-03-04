using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.DTO;
using WebShop.Enums;
using WebShop.Models;

namespace WebShop.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly AppDbContext _context;
        public OrderServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(OrderDTO order)
        {
            var cart = await _context.Carts
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.UserId == order.UserId);
            if (cart == null) return;
            var newOrder = new Order
            {
                Id = Guid.NewGuid(),
                UserId = order.UserId,
                Address = order.Address,
                TotalAmount = cart.TotalPrice,
                Status = Status.Created,
                CreatedAt = DateTime.UtcNow,
                Items = cart.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    PriceAtPurchase = i.UnitPrice
                }).ToList()
            };
            await _context.Products.Where(p => cart.Items.Select(i => i.ProductId).Contains(p.Id)).ForEachAsync(p =>
            {
                var cartItem = cart.Items.First(i => i.ProductId == p.Id);
                if (p.StockQuantity >= cartItem.Quantity)
                {
                    p.StockQuantity -= cartItem.Quantity;
                }
                else
                {
                    throw new Exception($"Ошибка при добавлении продукта");
                }
            });
            _context.Carts.Remove(cart);
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid OrderId)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == OrderId);

            if (order == null)
                return;

            foreach (var item in order.Items)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == item.ProductId);

                if (product != null)
                    product.StockQuantity += item.Quantity;
            }

            order.Status = Status.Cancelled;

            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderResponseDTO>> GetAll()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Select(o => new OrderResponseDTO
                {
                    UserId = o.UserId,
                    Address = o.Address,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    Items = o.Items.Select(i => new OrderItemsResponseDTO
                    {
                        ProductName = i.Product.Name,
                        Quantity = i.Quantity,
                        PriceAtPurchase = i.PriceAtPurchase
                    }).ToList()
                })
                .ToListAsync();
            return orders;
        }

        public async Task<OrderResponseDTO> GetById(Guid id)
        {
            var dto = await _context.Orders
                .Where(o => o.Id == id)
                .Select(o => new OrderResponseDTO
                {
                    UserId = o.UserId,
                    Address = o.Address,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    Items = o.Items.Select(i => new OrderItemsResponseDTO
                    {
                        ProductName = i.Product.Name,
                        Quantity = i.Quantity,
                        PriceAtPurchase = i.PriceAtPurchase
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            return dto;
        }

        public async Task Update(Guid orderId, OrderDTO order)
        {
            var existingOrder = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (existingOrder == null)
                return;

            existingOrder.Address = order.Address;

            await _context.SaveChangesAsync();
        }
    }
}
