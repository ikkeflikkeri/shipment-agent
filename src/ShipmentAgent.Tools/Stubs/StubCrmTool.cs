using Microsoft.Extensions.Logging;

namespace ShipmentAgent.Tools.Stubs;

/// <summary>
/// Local stub of the CRM tool. Looks up customers by email (in-memory),
/// sends notifications (logged only), and logs shipment events.
/// </summary>
public sealed class StubCrmTool : ICrmTool
{
    private readonly ILogger<StubCrmTool> _logger;
    private readonly Dictionary<string, CustomerLookup> _customers = new(StringComparer.OrdinalIgnoreCase)
    {
        ["buyer@acme.example"] = new("CUST-001", "ACME Logistics NV", "buyer@acme.example"),
    };

    public StubCrmTool(ILogger<StubCrmTool> logger)
    {
        _logger = logger;
    }

    public Task<CustomerLookup?> FindCustomerByEmailAsync(string email, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CRM lookup: email={Email}", email);
        _customers.TryGetValue(email, out var customer);
        return Task.FromResult<CustomerLookup?>(customer);
    }

    public Task<NotificationResult> NotifyCustomerAsync(
        string customerId,
        string subject,
        string body,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "CRM notify: customer={Customer} subject={Subject} bodyLength={BodyLength}",
            customerId, subject, body.Length);

        var result = new NotificationResult(
            NotificationId: $"NOTIF-{Guid.NewGuid():N}"[..10],
            Channel: "email",
            SentAt: DateTimeOffset.UtcNow);

        return Task.FromResult(result);
    }

    public Task LogShipmentEventAsync(
        string customerId,
        string eventType,
        IReadOnlyDictionary<string, string> details,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "CRM log: customer={Customer} event={Event} details={DetailCount}",
            customerId, eventType, details.Count);
        return Task.CompletedTask;
    }
}
