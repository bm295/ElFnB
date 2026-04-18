using Application.Models;
using Application.Ports;
using Domain.Entities;

namespace Application.UseCases;

public sealed class CreateOrderForTableUseCase
{
    private readonly IOrderRepository _orders;
    private readonly ITableRepository _tables;

    public CreateOrderForTableUseCase(IOrderRepository orders, ITableRepository tables)
    {
        _orders = orders;
        _tables = tables;
    }

    public async Task<OrderDto> ExecuteAsync(int tableNumber, CancellationToken cancellationToken)
    {
        var table = await _tables.GetByNumberAsync(tableNumber, cancellationToken);
        if (table is null)
        {
            throw new InvalidOperationException($"Table {tableNumber} does not exist.");
        }

        var activeOrders = await _orders.GetActiveAsync(cancellationToken);
        if (activeOrders.Any(x => x.TableNumber == tableNumber))
        {
            throw new InvalidOperationException($"Table {tableNumber} already has an active order.");
        }

        var order = new Order(Guid.NewGuid(), tableNumber, DateTimeOffset.UtcNow);
        await _orders.AddAsync(order, cancellationToken);

        return new OrderDto(order.Id, order.TableNumber, order.Status, order.TotalAmount, order.Items.Count);
    }
}
