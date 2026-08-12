using ShipmentAgent.Agents;
using ShipmentAgent.Infrastructure;
using ShipmentAgent.Tools;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddShipmentAgentInfrastructure(builder.Configuration);
builder.Services.AddShipmentAgentTools();
builder.Services.AddShipmentAgentAgents();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapPost("/shipments", async (
    ShipmentRequest request,
    ShipmentAgentRunner runner,
    CancellationToken cancellationToken) =>
{
    var outcome = await runner.RunAsync(request, cancellationToken);
    return Results.Ok(outcome);
});

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();

// For WebApplicationFactory in tests.
public partial class Program;