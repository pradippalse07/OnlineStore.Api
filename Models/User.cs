namespace OnlineStore.Api.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
}