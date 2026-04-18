namespace Application.Models;

public sealed record MenuItemDto(string Sku, string Name, int QuantityOnHand, decimal SuggestedPrice);
