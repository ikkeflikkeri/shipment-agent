# The demo story

This document is the buyer-facing version of the demo. Pair with the 5-minute video.

## Problem

A logistics operator receives a shipment request (email, form, API). A human reads it, opens the ERP, checks inventory, requests a carrier quote, books the pickup, drafts a confirmation, notifies the customer, and logs the event in the CRM.

Average handle time: 12 minutes. Errors: 4%. Boring work that still eats a workday.

## What the agent does

Receives the request. Reads it. Plans the work. Takes actions across multiple systems:

1. **ERP** — pulls the order, including SKU, quantity, destination, required-by date.
2. **CRM** — resolves the customer.
3. **Inventory** — checks stock availability.
4. **Carrier** — quotes rates, picks a carrier, books the pickup.
5. **CRM** — sends the confirmation.
6. **ERP** — records the shipment.
7. **CRM** — logs the event.

Every step is observable. Every action is auditable.

Average handle time: **~10 seconds**. Errors: **0** (the agent never typos an email address).

## Why hire me to build this

The agent is the obvious part. What's not obvious is what it takes to ship it inside your business:

- The agent lives **on your Azure, in your tenant**, with the same auth (Entra ID / MSAL), the same observability (App Insights / OpenTelemetry), the same message bus (Service Bus), the same key vault, the same deployment pipeline.
- The tools aren't toys — they call **your** ERP, **your** WMS, **your** carrier account. The stubs in this repo are just for the demo.
- The workflow shape is yours. The agent fills in reasoning, summarisation, and edge cases. **You own the audit trail.**
- It runs as one of your services, not a third-party tool.

This is **production-grade agent work**, built on the Microsoft stack a Belgian mid-market buyer already uses. Same muscle, sharpened.

## What this repo contains

- `src/ShipmentAgent.Host/` — ASP.NET Core host, one endpoint.
- `src/ShipmentAgent.Agents/` — the agent runner. Wires the workflow shape.
- `src/ShipmentAgent.Tools/` — typed tool contracts + local stubs.
- `src/ShipmentAgent.Infrastructure/` — config + telemetry plumbing.
- `tests/ShipmentAgent.Tests/` — xUnit tests of the end-to-end flow.
- `scripts/demo.sh` — drives the demo with a sample request.

## Stack

- Microsoft Agent Framework (agent runtime)
- Semantic Kernel (LLM orchestration)
- Azure OpenAI (reasoning model)
- Azure AI Search (retrieval over docs — wired but stubbed here)
- Azure Service Bus (orchestration — wired but stubbed here)
- Application Insights / OpenTelemetry (observability)
- .NET 10 / ASP.NET Core

## See it run

```bash
dotnet run --project src/ShipmentAgent.Host
# In another terminal:
bash scripts/demo.sh
```