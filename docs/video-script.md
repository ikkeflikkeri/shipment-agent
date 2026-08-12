# Video script — 5 minutes

Format: screen-recorded demo + voiceover. Cut between the running app and a few key files in the repo.

---

## 1. The problem (0:00 – 0:30)

Voiceover, on screen: a single screen capture of an Outlook inbox with shipment requests sitting unanswered.

> "Every logistics operator I work with has the same problem. Shipment requests land in an inbox. A person reads them, opens the ERP, checks inventory, requests a carrier quote, books the pickup, drafts a confirmation email, and logs the event in the CRM. Twelve minutes per request, four percent error rate, and it's boring enough to burn out the people doing it."

Cut to terminal:

> "The shape of this work is exactly what an agent is for."

---

## 2. The agent (0:30 – 2:30)

Terminal — `bash scripts/demo.sh`.

> "Here's the request."

Show `scripts/sample-request.json`:

> "One order id. That's it."

Show the JSON response — the eight steps, each with a timestamp and outcome:

> "Twelve minutes, in ten milliseconds. Eight steps. Every system touched. Audit trail attached."

Slow on the steps list as the voiceover reads them:

> "ERP — pulls the order. CRM — resolves the customer. Inventory — checks stock. Carrier — quotes and books. CRM — sends the confirmation. ERP — records the shipment. CRM — logs the event."

Pause on the tracking reference:

> "Notice: tracking reference, confirmed pickup date, estimated delivery — the agent didn't just run a workflow, it produced a usable outcome."

---

## 3. The stack (2:30 – 4:00)

Open the solution in VS Code. Walk through `src/`:

- **`ShipmentAgent.Host`** — one endpoint, ASP.NET Core.
- **`ShipmentAgent.Agents`** — the runner. Show `ShipmentAgentRunner.cs`, scroll through the eight steps.

> "The workflow shape is explicit. I know what the agent will do before it does it. The LLM fills in the parts that need reasoning — here, the customer notification."

Show `GenerateNotificationBodyAsync` — the kernel call:

> "Semantic Kernel + Azure OpenAI generates the email body. Falls back to a template if the endpoint is down. The workflow always completes."

- **`ShipmentAgent.Tools`** — open one, `ICarrierTool.cs`.

> "Four typed tool contracts. Inventory, carrier, CRM, ERP. Stubs run locally so you can demo without an Azure subscription. Real Azure clients slot in here — same interface, swap the implementation."

- **`ShipmentAgent.Infrastructure`** — show `ShipmentAgentOptions`, the OpenTelemetry wiring.

> "Configuration, validation, telemetry. Standard Microsoft.Extensions plumbing. App Insights in production."

- **Tests** — show `ShipmentAgentRunnerTests.cs`, run `dotnet test` briefly.

> "End-to-end test against stubs. Green in under a second."

---

## 4. The hire (4:00 – 5:00)

Open `docs/portfolio.md`. Scroll through.

> "This demo runs locally in two commands. The version in your business runs the same way, on your Azure, in your tenant, with your auth, your observability, your ERP, your CRM, your carrier accounts."

> "I'm Niels Wouters. Eleven years building enterprise software on .NET and Azure. The last two years on AI platforms — Umicore, Insurax, KBC. I build agent systems that actually do work inside your existing stack. If you're shipping shipment requests, claims, RFPs, onboarding flows, document pipelines — same shape, different industry."

> "Repo and details in the description. Let's talk."

End card: GitHub URL + email.

---

## Recording notes

- Total runtime: keep under 5 minutes. Cut anything that doesn't earn its time.
- Don't rehearse the terminal commands live — pre-record with the JSON visible, then voiceover.
- The eight-step output is the wow moment. Hold on it.
- The agent call (kernel call) is the proof point for "this is an AI system". Show the prompt, show the fallback.
- No marketing slides. Code and JSON.
