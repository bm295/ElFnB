namespace Application.Models;

public sealed record OrderLineDto(string Sku, string Name, int Quantity, decimal UnitPrice, decimal LineTotal);
