# Deployment (placeholder)

This folder will hold the Bicep / Aspire wiring that takes the agent from this repo to a buyer's Azure tenant.

Planned contents:

- `main.bicep` — Azure resource definitions (App Service, Service Bus, AI Search, App Insights, Key Vault).
- `app.bicep` — the agent host's resource definition.
- `aspire/` — Aspire AppHost for local dev with real Azure services.
- `pipelines/azure-pipelines.yml` — CI/CD placeholder.

Status: not started. The agent runs locally against stubs for the demo.

Swap-in order when moving to real Azure:

1. AI Search (replace stub with Azure AI Search index over order docs).
2. Service Bus (replace in-process runner dispatch with queue-driven orchestration).
3. ERP / CRM / Carrier (replace stubs with real clients — same tool contracts).
4. Auth (Entra ID / MSAL on the host endpoint).
5. Observability (Application Insights exporter wired in `Infrastructure`).