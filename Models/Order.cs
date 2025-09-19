namespace OnlineStore.Api.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required User User { get; set; }
    public DateTime OrderDate { get; set; }
    public required ICollection<OrderItem> OrderItems { get; set; }
}