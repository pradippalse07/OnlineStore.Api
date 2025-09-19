using Microsoft.EntityFrameworkCore;
using OnlineStore.Api.Models;

namespace OnlineStore.Api.Services;

public class CartService
{
    private readonly StoreContext _context;

    public CartService(StoreContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderAsync(Guid userId, List<CartItem> items)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Load all required products in a single query
            var productIds = items.Select(i => i.ProductId).Distinct().ToList();
            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            // Verify all products exist and have sufficient stock
            foreach (var item in items)
            {
                if (!products.TryGetValue(item.ProductId, out var product))
                {
                    throw new Exception($"Product not found: {item.ProductId}");
                }
                if (product.Stock < item.Quantity)
                {
                    throw new Exception($"Insufficient stock for product: {product.Name}");
                }
            }

            // Get user and create order
            var user = await _context.Users.FindAsync(userId) 
                ?? throw new Exception($"User not found: {userId}");
            
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                User = user,
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };

            // Create order items and update stock
            foreach (var item in items)
            {
                var product = products[item.ProductId];
                order.OrderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    Order = order,
                    ProductId = item.ProductId,
                    Product = product,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
                product.Stock -= item.Quantity;
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return order;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}

public class CartItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}