using Application.Ports;

namespace Adapters.Integrations;

public sealed class ConsoleKitchenNotifier : IKitchenNotifier
{
    public Task NotifyOrderSentAsync(Guid orderId, int tableNumber, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Kitchen ticket queued. Order={orderId}, Table={tableNumber}");
        return Task.CompletedTask;
    }
}
