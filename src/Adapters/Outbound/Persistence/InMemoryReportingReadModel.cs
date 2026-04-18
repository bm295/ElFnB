using Application.Models;
using Application.Ports;

namespace Adapters.Persistence;

public sealed class InMemoryReportingReadModel : IReportingReadModel
{
    private readonly IOrderRepository _orders;

    public InMemoryReportingReadModel(IOrderRepository orders)
    {
        _orders = orders;
    }

    public async Task<SalesReportDto> BuildDailySalesReportAsync(DateOnly day, CancellationToken cancellationToken)
    {
        var closedOrders = await _orders.GetClosedByDateAsync(day, cancellationToken);
        var revenue = closedOrders.Sum(x => x.TotalAmount);

        return new SalesReportDto(day, closedOrders.Count, revenue);
    }
}
