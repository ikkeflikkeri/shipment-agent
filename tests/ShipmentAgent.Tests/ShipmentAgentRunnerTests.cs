using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel;
using ShipmentAgent.Agents;
using ShipmentAgent.Tools.Stubs;
using Xunit;

namespace ShipmentAgent.Tests;

public class ShipmentAgentRunnerTests
{
    [Fact]
    public async Task RunAsync_completes_full_workflow_with_stubs()
    {
        // Arrange
        var inventory = new StubInventoryTool(NullLogger<StubInventoryTool>.Instance);
        var carrier = new StubCarrierTool(NullLogger<StubCarrierTool>.Instance);
        var crm = new StubCrmTool(NullLogger<StubCrmTool>.Instance);
        var erp = new StubErpTool(NullLogger<StubErpTool>.Instance);

        var kernel = Kernel.CreateBuilder().Build();
        var runner = new ShipmentAgentRunner(
            kernel, erp, inventory, carrier, crm, NullLogger<ShipmentAgentRunner>.Instance);

        var request = new ShipmentRequest(
            OrderId: "ORD-12345",
            CustomerEmail: "buyer@acme.example",
            Notes: "Leave at the loading dock.");

        // Act
        var outcome = await runner.RunAsync(request, CancellationToken.None);

        // Assert
        Assert.Equal("ORD-12345", outcome.OrderId);
        Assert.False(string.IsNullOrWhiteSpace(outcome.ShipmentId));
        Assert.False(string.IsNullOrWhiteSpace(outcome.TrackingReference));

        var stepNames = outcome.Steps.Select(s => s.Name).ToList();
        Assert.Contains("erp.lookup_order", stepNames);
        Assert.Contains("crm.lookup_customer", stepNames);
        Assert.Contains("inventory.check", stepNames);
        Assert.Contains("carrier.quote", stepNames);
        Assert.Contains("carrier.book", stepNames);
        Assert.Contains("crm.notify", stepNames);
        Assert.Contains("erp.record_shipment", stepNames);
        Assert.Contains("crm.log_event", stepNames);
    }
}