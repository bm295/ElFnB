using Application.Ports;
using Domain.Entities;

namespace Adapters.Persistence;

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly Dictionary<Guid, Order> _orders = [];

    public Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        _orders[order.Id] = order;
        return Task.CompletedTask;
    }

    public Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken)
    {
        _orders.TryGetValue(orderId, out var order);
        return Task.FromResult(order);
    }

    public Task UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        _orders[order.Id] = order;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Order>> GetClosedByDateAsync(DateOnly day, CancellationToken cancellationToken)
    {
        var result = _orders.Values
            .Where(x => x.Status == OrderStatus.Closed && DateOnly.FromDateTime(x.CreatedAtUtc.UtcDateTime) == day)
            .ToList()
            .AsReadOnly();

        return Task.FromResult<IReadOnlyCollection<Order>>(result);
    }
}
