# Architecture

## High-level

```
        ┌─────────────────────┐
        │   HTTP / event      │
        │   (email, form, API)│
        └──────────┬──────────┘
                   │
                   ▼
        ┌─────────────────────┐
        │  ShipmentAgent      │ ← Microsoft Agent Framework
        │  Runner (Kernel)    │ ← Semantic Kernel underneath
        └──────┬──────────────┘
               │
   ┌───────────┼───────────┬───────────┬───────────┐
   ▼           ▼           ▼           ▼           ▼
┌──────┐  ┌────────┐  ┌────────┐  ┌────────┐  ┌────────┐
│ ERP  │  │ CRM    │  │Inventry│  │Carrier │  │ Search │
│stub  │  │ stub   │  │ stub   │  │ stub   │  │ stub   │
└──────┘  └────────┘  └────────┘  └────────┘  └────────┘
   │           │           │           │           │
   └───── all real Azure services in production ─────┘
```

## Tool contracts

All four tools have a typed interface in `src/ShipmentAgent.Tools/`. The stubs implement them locally. Production swaps in the real client; the runner doesn't change.

- `IInventoryTool` — stock check by SKU + quantity + required-by.
- `ICarrierTool` — rate quote + booking.
- `ICrmTool` — customer lookup, notification, event logging.
- `IErpTool` — order lookup, shipment recording.

## Workflow shape

The runner (`ShipmentAgentRunner`) defines the workflow explicitly in code. Every step is recorded with a name and outcome, returned in `ShipmentOutcome.Steps` for the audit trail.

Why explicit instead of letting the LLM plan every step:

- **Auditability** — a buyer needs to know what the agent will do before it does it.
- **Predictability** — the workflow shape is yours; the LLM fills in reasoning.
- **Reviewability** — a code reviewer reads `RunAsync` top to bottom and sees the entire flow.

The Microsoft Agent Framework typed agents and Semantic Kernel tool calling provide the runtime — the runner captures intent in code.

## Observability

OpenTelemetry hooks into the host via `Microsoft.Extensions.Hosting`. The runner logs every step with structured logging. In production, App Insights ingests these.

## Configuration

`ShipmentAgentOptions` is bound from `appsettings.json`, environment variables, or Azure App Configuration. The runner uses no environment-specific code paths.

## Deployment

The `deploy/` directory will hold the Bicep / Aspire wiring for production. Currently a placeholder — see `docs/portfolio.md` for the intended shape.