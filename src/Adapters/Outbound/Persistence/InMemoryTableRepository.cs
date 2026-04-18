using Application.Ports;
using Domain.Entities;
using Domain.Services;

namespace Adapters.Persistence;

public sealed class InMemoryTableRepository : ITableRepository
{
    private readonly List<Table> _tables;

    public InMemoryTableRepository(int totalSeats = 100)
    {
        RestaurantCapacityPolicy.EnsureWithinRange(totalSeats);

        _tables =
        [
            new Table(1, 6), new Table(2, 6), new Table(3, 4), new Table(4, 4), new Table(5, 8),
            new Table(6, 8), new Table(7, 10), new Table(8, 10), new Table(9, 12), new Table(10, 12),
            new Table(11, 10), new Table(12, 10)
        ];

        var configuredSeats = _tables.Sum(x => x.Seats);
        if (configuredSeats > totalSeats)
        {
            throw new InvalidOperationException("Configured table seats exceed restaurant capacity.");
        }
    }

    public Task<IReadOnlyCollection<Table>> GetAllAsync(CancellationToken cancellationToken)
        => Task.FromResult<IReadOnlyCollection<Table>>(_tables.AsReadOnly());

    public Task<Table?> GetByNumberAsync(int tableNumber, CancellationToken cancellationToken)
        => Task.FromResult(_tables.SingleOrDefault(x => x.Number == tableNumber));
}
