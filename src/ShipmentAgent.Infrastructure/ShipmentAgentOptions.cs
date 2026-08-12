using System.ComponentModel.DataAnnotations;

namespace ShipmentAgent.Infrastructure;

/// <summary>
/// Top-level configuration for the shipment agent host.
/// Bound from configuration (appsettings.json, environment variables, etc).
/// </summary>
public sealed class ShipmentAgentOptions
{
    public const string SectionName = "ShipmentAgent";

    [Required]
    public string ModelDeploymentName { get; init; } = "gpt-4o";

    [Required]
    public string Endpoint { get; init; } = "https://localhost:5001/v1";

    public bool UseLocalStubs { get; init; } = true;
}
