namespace ShipmentAgent.Agents;

/// <summary>
/// Inbound shipment request as received from the channel (email, form, API).
/// The agent uses this together with the ERP order to plan the work.
/// </summary>
public sealed record ShipmentRequest(
    string OrderId,
    string CustomerEmail,
    string? Notes);

/// <summary>
/// Final outcome of a shipment run.
/// </summary>
public sealed record ShipmentOutcome(
    string OrderId,
    string ShipmentId,
    string CarrierName,
    string TrackingReference,
    DateOnly ConfirmedPickupDate,
    DateOnly EstimatedDeliveryDate,
    IReadOnlyList<ShipmentStep> Steps);

public sealed record ShipmentStep(
    string Name,
    DateTimeOffset At,
    string Outcome);