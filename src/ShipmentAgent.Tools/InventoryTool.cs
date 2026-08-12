namespace ShipmentAgent.Tools;

/// <summary>
/// Request to check inventory availability.
/// </summary>
public sealed record InventoryCheckRequest(
    string Sku,
    int Quantity,
    DateOnly RequiredBy);

/// <summary>
/// Result of an inventory check. Holds a short list of warehouses that can fulfil.
/// </summary>
public sealed record InventoryCheckResult(
    string Sku,
    int Requested,
    bool Available,
    IReadOnlyList<WarehouseAllocation> Allocations);

public sealed record WarehouseAllocation(
    string WarehouseId,
    string WarehouseName,
    int Quantity,
    DateOnly ShipByDate);

/// <summary>
/// Tool contract: the agent calls this to determine stock availability
/// before committing to a shipment.
/// </summary>
public interface IInventoryTool
{
    Task<InventoryCheckResult> CheckAsync(
        InventoryCheckRequest request,
        CancellationToken cancellationToken);
}
