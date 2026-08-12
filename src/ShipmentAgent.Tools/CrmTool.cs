namespace ShipmentAgent.Tools;

public sealed record CustomerLookup(string CustomerId, string Name, string Email);

public sealed record NotificationResult(string NotificationId, string Channel, DateTimeOffset SentAt);

public interface ICrmTool
{
    Task<CustomerLookup?> FindCustomerByEmailAsync(string email, CancellationToken cancellationToken);

    Task<NotificationResult> NotifyCustomerAsync(
        string customerId,
        string subject,
        string body,
        CancellationToken cancellationToken);

    Task LogShipmentEventAsync(
        string customerId,
        string eventType,
        IReadOnlyDictionary<string, string> details,
        CancellationToken cancellationToken);
}
