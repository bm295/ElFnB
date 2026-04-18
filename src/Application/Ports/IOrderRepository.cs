using Domain.Entities;

namespace Application.Ports;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken);
    Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Order>> GetActiveAsync(CancellationToken cancellationToken);
    Task UpdateAsync(Order order, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Order>> GetClosedByDateAsync(DateOnly day, CancellationToken cancellationToken);
}
