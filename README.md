# Shipment Agent

A multi-system workflow agent that takes inbound shipment requests, plans the work, and takes actions across inventory, carrier, CRM and ERP systems.

Built as a portfolio demonstration of **production-grade agent systems on Azure**. The agent runs on [Microsoft Agent Framework](https://learn.microsoft.com/en-us/agent-framework/) with [Semantic Kernel](https://learn.microsoft.com/en-us/semantic-kernel/) underneath, and is designed to be deployed into a buyer's existing .NET / Azure estate with the same auth, observability and security as their other systems.

## What it does

1. **Receives** a shipment request (email / form / API).
2. **Reads** the request and any attached order docs.
3. **Plans** a multi-step workflow: check inventory, pick a carrier, schedule a pickup, notify the customer, log to CRM/ERP.
4. **Acts** on each step, using tools to call the relevant system.
5. **Reports** the outcome, with an audit trail.

Every step is a typed agent action. The agent's reasoning, tool calls and outcomes are observable.

## Why this demo

This is not a chatbot. It is an agent doing real work across multiple systems, on the Microsoft agent stack a Belgian mid-market buyer would actually adopt.

See `docs/portfolio.md` for the full demo story.

## Stack

- **Microsoft Agent Framework** — agent runtime (typed agents, threads, tool calling)
- **Semantic Kernel** — LLM orchestration underneath
- **Azure OpenAI** — reasoning model
- **Azure AI Search** — retrieval over order docs (stubbed in this repo)
- **Azure Service Bus** — orchestration queue (optional, stubbed in this repo)
- **Application Insights / OpenTelemetry** — observability
- **.NET 10 / ASP.NET Core** — host

For this demo, all integrations (inventory, carrier, CRM, ERP, AI Search, Service Bus) are **stubbed locally** so the demo runs with zero external accounts. The stub boundaries are the same shape as the real integrations, so swapping in Azure services is a configuration change, not a rewrite.

## Repository layout

```
src/
  ShipmentAgent.Host/        ASP.NET Core host (single endpoint, agent runtime)
  ShipmentAgent.Agents/      Microsoft Agent Framework agent definitions
  ShipmentAgent.Tools/       Tools the agent calls (typed, one file per system)
  ShipmentAgent.Infrastructure/  Shared infra (telemetry, config, error handling)
stubs/
  inventory/                 Local pseudo-service: stock check
  carrier/                   Local pseudo-service: rate + schedule
  crm/                       Local pseudo-service: customer + log
  erp/                       Local pseudo-service: order + log
deploy/                      Bicep / Aspire deployment (placeholder, real Azure wiring TBD)
docs/
  portfolio.md               The demo story (problem, what it does, why hire)
  architecture.md            How the agent is wired
scripts/
  demo.sh                    Drive the demo end-to-end with a sample request
tests/
  ShipmentAgent.Tests/       xUnit tests
```

## Running the demo

```bash
dotnet run --project src/ShipmentAgent.Host
# In another terminal:
curl -X POST http://localhost:5000/shipments -H 'Content-Type: application/json' -d @scripts/sample-request.json
```

The host returns the agent's plan + the action log.

## Status

🚧 **Scaffold only.** Implementation in progress. See `docs/portfolio.md` for the intended shape.

## License

MIT.
