using Microsoft.Extensions.Logging;

namespace ShipmentAgent.Tools.Stubs;

/// <summary>
/// Local stub of the ERP tool. Returns a fixed order for the demo order id.
/// Replace with the real ERP client (SAP, Dynamics, Oracle, ...) in production.
/// </summary>
public sealed class StubErpTool : IErpTool
{
    private readonly ILogger<StubErpTool> _logger;

    public StubErpTool(ILogger<StubErpTool> logger)
    {
        _logger = logger;
    }

    public Task<ErpOrder?> FindOrderAsync(string orderId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ERP find order: id={OrderId}", orderId);

        var order = new ErpOrder(
            OrderId: orderId,
            Sku: "SKU-WIDGET-001",
            Quantity: 12,
            DestinationPostalCode: "2000",
            DestinationCountry: "BE",
            RequiredBy: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3)),
            CustomerEmail: "buyer@acme.example");

        return Task.FromResult<ErpOrder?>(order);
    }

    public Task<string> RecordShipmentAsync(
        string orderId,
        string trackingReference,
        DateOnly pickupDate,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "ERP record shipment: order={Order} tracking={Tracking} pickup={Pickup}",
            orderId, trackingReference, pickupDate);

        return Task.FromResult($"SHIP-{Guid.NewGuid():N}"[..12].ToUpperInvariant());
    }
}
