using Microsoft.Extensions.Logging;

namespace ShipmentAgent.Tools.Stubs;

/// <summary>
/// Local stub of the inventory tool. Returns plausible allocations based on the
/// SKU prefix. Replace with a real ERP / WMS client in production.
/// </summary>
public sealed class StubInventoryTool : IInventoryTool
{
    private readonly ILogger<StubInventoryTool> _logger;

    public StubInventoryTool(ILogger<StubInventoryTool> logger)
    {
        _logger = logger;
    }

    public Task<InventoryCheckResult> CheckAsync(
        InventoryCheckRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Inventory check: SKU={Sku} Qty={Qty} RequiredBy={RequiredBy}",
            request.Sku, request.Quantity, request.RequiredBy);

        var allocations = new List<WarehouseAllocation>
        {
            new("WH-ANT", "Antwerp DC", request.Quantity, request.RequiredBy.AddDays(-2)),
        };

        var result = new InventoryCheckResult(
            request.Sku,
            request.Quantity,
            Available: true,
            Allocations: allocations);

        return Task.FromResult(result);
    }
}
