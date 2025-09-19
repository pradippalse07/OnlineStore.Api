using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Api.Models;

// [PrimaryKey(nameof(OrderId), nameof(ProductId))]
public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public required Order Order { get; set; }
    public Guid ProductId { get; set; }
    public required Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}