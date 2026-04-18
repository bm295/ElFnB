using Application.Models;
using Application.Ports;

namespace Adapters.Integrations;

public sealed class FakePaymentGateway : IPaymentGateway
{
    public Task<PaymentResult> ChargeAsync(decimal amount, string method, CancellationToken cancellationToken)
    {
        if (amount <= 0)
        {
            return Task.FromResult(new PaymentResult(false, string.Empty, "Amount must be greater than zero."));
        }

        var transactionId = $"TXN-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
        return Task.FromResult(new PaymentResult(true, transactionId, "Approved"));
    }
}
