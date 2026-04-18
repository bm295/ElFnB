using System.ComponentModel.DataAnnotations;
using Application.Models;

namespace Web.Models;

public sealed record OperationsDashboardViewModel(
    OperationsDashboardDto Dashboard,
    IReadOnlyCollection<string> PaymentMethods);

public sealed class CreateOrderInputModel
{
    [Range(1, int.MaxValue)]
    public int TableNumber { get; set; }
}

public sealed class AddOrderItemInputModel
{
    [Required]
    public Guid OrderId { get; set; }

    [Required]
    public string Sku { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; } = 1;
}

public sealed class RemoveOrderItemInputModel
{
    [Required]
    public Guid OrderId { get; set; }

    [Required]
    public string Sku { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; } = 1;
}

public sealed class PayOrderInputModel
{
    [Required]
    public Guid OrderId { get; set; }

    [Required]
    public string Method { get; set; } = string.Empty;
}
