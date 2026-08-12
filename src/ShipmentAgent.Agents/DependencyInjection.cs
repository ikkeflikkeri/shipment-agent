using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using ShipmentAgent.Infrastructure;

namespace ShipmentAgent.Agents;

public static class DependencyInjection
{
    /// <summary>
    /// Registers the agent kernel and the runner. The kernel is wired with the
    /// underlying model deployment; tools are passed in as a plugin so the agent
    /// (Microsoft Agent Framework) can call them via Semantic Kernel's tool
    /// calling surface.
    /// </summary>
    public static IServiceCollection AddShipmentAgentAgents(
        this IServiceCollection services)
    {
        services.AddSingleton<Kernel>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<ShipmentAgentOptions>>().Value;

            var builder = Kernel.CreateBuilder();
            builder.AddAzureOpenAIChatCompletion(
                deploymentName: options.ModelDeploymentName,
                endpoint: options.Endpoint,
                apiKey: "demo"); // demo only — real key from Key Vault / managed identity in production

            return builder.Build();
        });

        services.AddSingleton<ShipmentAgentRunner>();
        return services;
    }
}