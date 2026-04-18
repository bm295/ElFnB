using Domain.Entities;

namespace Application.Models;

public sealed record ActiveOrderDto(
    Guid Id,
    int TableNumber,
    DateTimeOffset CreatedAtUtc,
    OrderStatus Status,
    decimal TotalAmount,
    IReadOnlyCollection<OrderLineDto> Items,
    PaymentSummaryDto? Payment);
