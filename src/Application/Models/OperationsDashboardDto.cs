namespace Application.Models;

public sealed record OperationsDashboardDto(
    DateOnly Day,
    SalesReportDto DailySales,
    IReadOnlyCollection<DiningTableDto> Tables,
    IReadOnlyCollection<ActiveOrderDto> ActiveOrders,
    IReadOnlyCollection<MenuItemDto> MenuItems);
