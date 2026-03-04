using WebShop.Data;
using WebShop.Models;
using System.Threading.Tasks;
using WebShop.DTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace WebShop.Services
{
    public class CartServices : ICartServices
    {
        private readonly AppDbContext _context;
        public CartServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Product product, string userId)
        {
            var existingCart = await TryGetByUserId(userId);

            if (existingCart == null && product.IsActive)
            {
                existingCart = new Cart()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = new List<CartItem>() {
                        new CartItem()
                        {
                            Id = Guid.NewGuid(),
                            ProductId = product.Id,
                            Quantity = 1,
                            UnitPrice = product.Price
                        }
                    },
                };
                _context.Carts.Add(existingCart);
            }
            else if (existingCart == null && !product.IsActive)
            {
                return;
            }
            else
            {
                var existingCartItem = existingCart.Items.FirstOrDefault(item =>
                    item.ProductId == product.Id);

                if (existingCartItem == null)
                {
                    var newCartItem = new CartItem
                    {
                        Id = Guid.NewGuid(),
                        CartId = existingCart.Id,
                        ProductId = product.Id,
                        Quantity = 1,
                        UnitPrice = product.Price
                    };
                    _context.CartItems.Add(newCartItem);
                }
                else
                {
                    existingCartItem.Quantity++;
                }
            }
            existingCart.TotalPrice = existingCart.Items.Sum(item => item.Quantity * item.UnitPrice);
            await _context.SaveChangesAsync();
        }

        public async Task Clear(string userId)
        {
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);
            var cartItems = await _context.CartItems
                .Where(ci => ci.CartId == cart.Id).ToListAsync();
            if (cart == null) return;
            if (cartItems == null) return;
            _context.CartItems.RemoveRange(cartItems);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        public async Task Substract(Guid productId, string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);   
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.Cart.UserId == userId && ci.ProductId == productId);
            if (cartItem == null) return;
            if (cart == null) return;
            cartItem.Quantity--;
            if(cartItem.Quantity == 0)
            {
                _context.CartItems.Remove(cartItem);
            }
            cart.TotalPrice = cart.Items.Sum(item => item.Quantity * item.UnitPrice);
            await _context.SaveChangesAsync();
        }

        public CartResponseDTO TryGetByUserIdResponse(Cart cart)
        {
            return new CartResponseDTO()
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                Items = cart.Items.Select(item => new CartItemsResponseDTO()
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };
        }

        public async Task<Cart?> TryGetByUserId(string UserId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == UserId);
        }
    }
}
