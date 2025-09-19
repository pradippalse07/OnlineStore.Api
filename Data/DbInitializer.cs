using Microsoft.EntityFrameworkCore;
using OnlineStore.Api.Models;

namespace OnlineStore.Api.Data;

public static class DbInitializer
{
    public static async Task Initialize(StoreContext context)
    {
        await context.Database.EnsureCreatedAsync();

        // Check if we already have products
        if (await context.Products.AnyAsync())
        {
            return; // DB has already been seeded
        }

        // Add sample products
        var products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Laptop",
                Price = 999.99m,
                Stock = 10
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Smartphone",
                Price = 499.99m,
                Stock = 20
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Headphones",
                Price = 99.99m,
                Stock = 30
            }
        };

        await context.Products.AddRangeAsync(products);

        // Add a test user
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "testuser"
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }
}