using Microsoft.Extensions.DependencyInjection;
using ShipmentAgent.Tools.Stubs;

namespace ShipmentAgent.Tools;

public static class DependencyInjection
{
    /// <summary>
    /// Registers the stub tool implementations. In production this would be
    /// swapped for real Azure-connected clients (AI Search, Service Bus, etc).
    /// </summary>
    public static IServiceCollection AddShipmentAgentTools(this IServiceCollection services)
    {
        services.AddSingleton<IInventoryTool, StubInventoryTool>();
        services.AddSingleton<ICarrierTool, StubCarrierTool>();
        services.AddSingleton<ICrmTool, StubCrmTool>();
        services.AddSingleton<IErpTool, StubErpTool>();
        return services;
    }
}
