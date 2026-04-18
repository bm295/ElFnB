using Domain.Entities;

namespace Application.Models;

public sealed record DiningTableDto(
    int Number,
    int Seats,
    bool HasActiveOrder,
    Guid? ActiveOrderId,
    OrderStatus? ActiveOrderStatus,
    decimal? ActiveOrderTotal);
