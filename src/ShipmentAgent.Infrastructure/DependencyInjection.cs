using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShipmentAgent.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddShipmentAgentInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<ShipmentAgentOptions>()
            .Bind(configuration.GetSection(ShipmentAgentOptions.SectionName));

        return services;
    }
}
