using Adapters.Integrations;
using Adapters.Persistence;
using Application.Ports;
using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRestaurantManagement(this IServiceCollection services)
    {
        services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
        services.AddSingleton<ITableRepository>(_ => new InMemoryTableRepository(totalSeats: 100));
        services.AddSingleton<IInventoryRepository, InMemoryInventoryRepository>();
        services.AddSingleton<IPaymentGateway, FakePaymentGateway>();
        services.AddSingleton<IKitchenNotifier, ConsoleKitchenNotifier>();
        services.AddSingleton<IReportingReadModel, InMemoryReportingReadModel>();

        services.AddTransient<CreateOrderForTableUseCase>();
        services.AddTransient<AddOrderItemUseCase>();
        services.AddTransient<RemoveOrderItemUseCase>();
        services.AddTransient<SendOrderToKitchenUseCase>();
        services.AddTransient<ProcessPaymentUseCase>();
        services.AddTransient<CloseOrderUseCase>();
        services.AddTransient<GetDailySalesReportUseCase>();
        services.AddTransient<GetOperationsDashboardUseCase>();

        return services;
    }
}
