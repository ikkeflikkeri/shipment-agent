namespace ShipmentAgent.Tools;

public sealed record ErpOrder(
    string OrderId,
    string Sku,
    int Quantity,
    string DestinationPostalCode,
    string DestinationCountry,
    DateOnly RequiredBy,
    string CustomerEmail);

public interface IErpTool
{
    Task<ErpOrder?> FindOrderAsync(string orderId, CancellationToken cancellationToken);

    Task<string> RecordShipmentAsync(
        string orderId,
        string trackingReference,
        DateOnly pickupDate,
        CancellationToken cancellationToken);
}
