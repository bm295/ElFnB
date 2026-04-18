namespace Application.Models;

public sealed record PaymentSummaryDto(decimal Amount, string Method, DateTimeOffset PaidAtUtc, string TransactionId);
