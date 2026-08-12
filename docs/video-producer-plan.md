# Producer-side plan — `shipment-agent` 5-min demo

## 1. Pre-record checklist

**Windows**
- Display scaling 100% (Settings → Display → Scale → 100%). 125%/150% kills terminal readability in 1080p capture.
- Resolution: native 1920x1080. Recording region locked to that.
- Focus Assist = **Alarms only**. Hide Teams / Outlook / Slack / OneDrive toasts.
- Power plan: High performance, plugged in, screen never sleeps.
- Close OneDrive sync popups, every Chrome tab unrelated to the demo, anything with toast notifications.
- Clean desktop: no icons, no wallpaper (or solid colour). Taskbar pinned to Terminal, VS Code, Chrome only.
- Optional: hide taskbar clock if it looks cluttered.

**Terminal (Windows Terminal, Git Bash profile)**
- Dark theme, high contrast. Font: **Cascadia Code** or **JetBrains Mono**, 14–16pt.
- Width ~120 cols, padding 0. Steady block cursor.
- One pinned tab, CWD = `C:\Users\Niels\experiments\weiner\shipment-agent`.
- Pre-run `bash scripts/demo.sh` once before recording. Confirm the eight steps show up. Don't start with the host already booted if it'll surprise you.

**VS Code**
- Profile: Dark+, Cascadia Code 14pt, **minimap OFF**, breadcrumb OFF, sticky scroll OFF.
- Workspace: only the `shipment-agent` folder. Close welcome tab and suggestion popups.
- Only the C# extension visible. No status-bar noise.

**Browser (Cut 1 only)**
- Chrome, single window, single tab — mock Outlook inbox page (see shot list).
- Bookmarks bar hidden.

**Sample data**
- `scripts/sample-request.json` open in VS Code, visible with a single click.
- Terminal window sized so the eight-step JSON output fits without horizontal scroll.

**Housekeeping**
- Phone silent, in another room.
- Script printed on paper next to mic. Water within reach.
- One dry run end-to-end with no recording. Lock in the JSON shape.

## 2. Scene-by-scene shot list

### Cut 1 — Problem (0:00 → 0:30, ~30s)
1. **Outlook inbox (15s)** — fake / mocked page, 6–8 unread shipment request emails, static or slow pan. VO over it.
2. **Hard cut to terminal (5s)** — prompt visible, command `bash scripts/demo.sh` typed (cursor still blinking). VO lead-in.
3. **Black / silence pause (5s)** — VO: "The shape of this work is exactly what an agent is for."

### Cut 2 — Agent (0:30 → 2:30, ~120s)
1. **Run demo (5s)** — `bash scripts/demo.sh` executes. curl hits the host, JSON streams in.
2. **Open `scripts/sample-request.json` in VS Code (5s)** — show `{ "orderId": "ORD-12345", … }`. VO: "One order id. That's it."
3. **Cut to terminal — full JSON response (20s)** — hold. The eight steps are the wow moment.
4. **Slow cursor walk through the eight steps (40s)** — ERP → CRM → Inventory → Carrier → CRM → ERP → CRM. VO lists each.
5. **Pause on tracking reference block (15s)** — `trackingReference`, `confirmedPickupDate`, `estimatedDelivery`. VO: "the agent didn't just run a workflow, it produced a usable outcome."
6. **Buffer (5s)** — frame the run.

### Cut 3 — Stack (2:30 → 4:00, ~90s)
1. **Solution Explorer in VS Code, `src/` expanded (5s)** — show the four projects.
2. **`ShipmentAgent.Host`** (8s) — open `Program.cs` briefly. VO: "one endpoint, ASP.NET Core."
3. **`src/ShipmentAgent.Agents/ShipmentAgentRunner.cs`** (25s) — scroll through the eight step invocations. VO: explicit workflow, LLM fills in the parts that need reasoning.
4. **`GenerateNotificationBodyAsync`** (15s) — kernel call + fallback. VO: "Semantic Kernel + Azure OpenAI … falls back to a template if the endpoint is down."
5. **`src/ShipmentAgent.Tools/ICarrierTool.cs`** (10s) — typed contract. VO: "Four typed tool contracts. Inventory, carrier, CRM, ERP. Stubs run locally."
6. **`src/ShipmentAgent.Infrastructure/ShipmentAgentOptions.cs`** (7s) — config + telemetry. VO: "Configuration, validation, telemetry. App Insights in production."
7. **Tests** (15s) — open `ShipmentAgentRunnerTests.cs`, integrated terminal → `dotnet test`, green tick. VO: "End-to-end test against stubs. Green in under a second."
8. **Buffer (5s)**.

### Cut 4 — Hire (4:00 → 5:00, ~60s)
1. **Open `docs/portfolio.md`** (5s) — scroll through.
2. **VO close (40s)** — "I'm Niels Wouters …", over the markdown source.
3. **End card (15s)** — frozen frame: `github.com/ikkeflikkeri/shipment-agent` + `niels@wwx.be`. VO tail: "Repo and details in the description. Let's talk."

## 3. Tooling

| Need | Tool | Cost |
| --- | --- | --- |
| Screen recorder | **OBS Studio 28+** — single scene, display capture, x264, CQP 18–20, MP4 | Free |
| Voiceover | **Audacity** + USB condenser (Fifine AM8 / Rode NT-USB Mini). Separate tracks per cut | Free / one-off |
| Editing | **DaVinci Resolve** (free tier) — cuts, captions, end-card compositing. Don't reach for Premiere | Free |
| End-card art | **Figma** or **Canva** — single PNG, dropped onto a clip in Resolve. Or do it inline in Resolve with Text+ | Free tier / free |
| Captions | Resolve's subtitle tool, or upload to **YouTube Studio** and use auto-captions, then re-embed `.srt` | Free |
| Headphones | Closed-back (Sony MDR-7506 or similar) to catch plosives live | One-off |

Skip: Camtasia (paid), Snagit (paid), Adobe anything, Riverside / Descript (overkill + recurring).

## 4. Recording session plan — single 90-min block

- **00–10 min — Setup**
  - Run the checklist above. Mic levels in Audacity, peaks ~ -12 dB, no clipping.
  - OBS: scene = full display capture, audio = mic, recording to `C:\recordings\shipment-agent-take-N.mp4`.
  - One dry run, no recording.
- **10–35 min — Record all four cuts top-to-bottom**
  - Cut 1 — 2 takes.
  - Cut 2 — 3 takes. Host frozen between takes; the JSON must match.
  - Cut 3 — 2 takes.
  - Cut 4 — 2 takes (one as a safety in case the end-card timing is off).
- **35–55 min — Voiceover pass**
  - Record VO separately in Audacity, reading the script cold.
  - One pass per cut, then a punch-in for any flubbed line.
  - Save stems as `vo-problem.wav`, `vo-agent.wav`, `vo-stack.wav`, `vo-hire.wav`.
- **55–75 min — Re-record the bad cuts**
  - Watch each take back-to-back. Flag the bad ones.
  - Re-shoot only the bad cuts. Don't re-record the whole thing because of one stumble.
- **75–90 min — Edit + export**
  - Import VO + best takes into Resolve. Lay VO over visuals, sync cuts to script beats.
  - End-card in, fade-out on tail. Export (see Export section).

## 5. Export

- **Format**: MP4, H.264, AAC stereo, **1920x1080 @ 30 fps**, ~12 Mbps target bitrate.
- **Captions**: yes — auto-generate in YouTube Studio post-upload, download `.srt`, re-embed via Resolve. Soft subs preferred over burned-in for accessibility.
- **End-card**: 5s static frame, baked into the MP4.
- **Distribution**:
  - **YouTube — unlisted**, link in repo README. Promote to public later when you want share-of-voice.
  - **GitHub release asset** — attach the MP4 to a `v1.0.0-demo` release. Embed via `<video>` in README pointing at the release asset URL so the repo has a downloadable copy.
  - **LinkedIn post** later: link to the YouTube URL.
- **Don't** upload to Vimeo (paywall) or any "AI consultancy portfolio" SaaS. YouTube + GitHub covers it.