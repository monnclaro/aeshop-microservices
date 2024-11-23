namespace Basket.Models;

public class ShoppingCart
{
    public string Username { get; set; }
    public decimal TotalPrice => Items.Sum(l => l.Price * l.Quantity);
    public List<ShoppingCartItem> Items { get; set; } = new();
    
    public void UpdateUsername(string username)
    {
        Username = username;
    }
}