using Application.Ports;
using Domain.Entities;

namespace Adapters.Persistence;

public sealed class InMemoryInventoryRepository : IInventoryRepository
{
    private readonly Dictionary<string, InventoryItem> _items = new(StringComparer.OrdinalIgnoreCase)
    {
        ["RIBEYE"] = new InventoryItem("RIBEYE", "Ribeye Steak", 200),
        ["WINE"] = new InventoryItem("WINE", "House Wine", 150),
        ["WATER"] = new InventoryItem("WATER", "Sparkling Water", 500)
    };

    public Task<InventoryItem?> GetBySkuAsync(string sku, CancellationToken cancellationToken)
    {
        _items.TryGetValue(sku, out var item);
        return Task.FromResult(item);
    }

    public Task UpdateAsync(InventoryItem item, CancellationToken cancellationToken)
    {
        _items[item.Sku] = item;
        return Task.CompletedTask;
    }
}
