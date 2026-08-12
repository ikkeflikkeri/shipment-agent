using Microsoft.Extensions.Logging;

namespace ShipmentAgent.Tools.Stubs;

/// <summary>
/// Local stub of the carrier tool. Returns a single quote from a fictional carrier.
/// Replace with a real carrier API (DHL, PostNL, etc) in production.
/// </summary>
public sealed class StubCarrierTool : ICarrierTool
{
    private readonly ILogger<StubCarrierTool> _logger;

    public StubCarrierTool(ILogger<StubCarrierTool> logger)
    {
        _logger = logger;
    }

    public Task<IReadOnlyList<CarrierQuote>> QuoteAsync(
        CarrierQuoteRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Carrier quote: origin={Origin} dest={Dest} weight={Weight}kg",
            request.OriginWarehouseId, request.DestinationPostalCode, request.WeightKg);

        IReadOnlyList<CarrierQuote> quotes = new[]
        {
            new CarrierQuote(
                CarrierId: "CARR-DEMO",
                CarrierName: "DemoExpress",
                Cost: 49.95m,
                Currency: "EUR",
                EstimatedPickupDate: request.RequiredBy.AddDays(-1),
                EstimatedDeliveryDate: request.RequiredBy),
        };

        return Task.FromResult(quotes);
    }

    public Task<CarrierBooking> BookAsync(
        string carrierId,
        string carrierName,
        string originWarehouseId,
        string destinationPostalCode,
        DateOnly pickupDate,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Carrier book: carrier={Carrier} origin={Origin} dest={Dest} pickup={Pickup}",
            carrierId, originWarehouseId, destinationPostalCode, pickupDate);

        var booking = new CarrierBooking(
            BookingId: $"BK-{Guid.NewGuid():N}"[..10],
            CarrierId: carrierId,
            CarrierName: carrierName,
            TrackingReference: $"TRK-{Guid.NewGuid():N}"[..14].ToUpperInvariant(),
            ConfirmedPickupDate: pickupDate);

        return Task.FromResult(booking);
    }
}
