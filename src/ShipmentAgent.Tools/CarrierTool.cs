namespace ShipmentAgent.Tools;

/// <summary>
/// Request to get carrier rates and schedule a pickup.
/// </summary>
public sealed record CarrierQuoteRequest(
    string OriginWarehouseId,
    string DestinationPostalCode,
    string DestinationCountry,
    int WeightKg,
    int VolumeM3,
    DateOnly RequiredBy);

public sealed record CarrierQuote(
    string CarrierId,
    string CarrierName,
    decimal Cost,
    string Currency,
    DateOnly EstimatedPickupDate,
    DateOnly EstimatedDeliveryDate);

/// <summary>
/// Result of booking a carrier pickup.
/// </summary>
public sealed record CarrierBooking(
    string BookingId,
    string CarrierId,
    string CarrierName,
    string TrackingReference,
    DateOnly ConfirmedPickupDate);

public interface ICarrierTool
{
    Task<IReadOnlyList<CarrierQuote>> QuoteAsync(
        CarrierQuoteRequest request,
        CancellationToken cancellationToken);

    Task<CarrierBooking> BookAsync(
        string carrierId,
        string carrierName,
        string originWarehouseId,
        string destinationPostalCode,
        DateOnly pickupDate,
        CancellationToken cancellationToken);
}
