# Recording-day checklist — `shipment-agent` 5-min demo

Print this. Tick it off in real time. If it's not on this list, don't improvise.

---

## 1. The night before

- [ ] **Display scaling → 100%** (Settings → Display → Scale). 125% kills terminal legibility at 1080p.
- [ ] **Resolution locked 1920x1080**. Recording region matches.
- [ ] **Power plan → High performance**, plugged in, screen never sleeps.
- [ ] **Focus Assist → Alarms only**. Suppress Teams / Outlook / Slack / OneDrive toasts.
- [ ] **OneDrive sync popups closed**. Every Chrome tab unrelated to the demo killed.
- [ ] **Desktop clean**: no icons, solid wallpaper. Taskbar pinned to Terminal, VS Code, Chrome only. Clock hidden if cluttered.
- [ ] **Windows Terminal** (Git Bash profile): dark, high contrast, Cascadia Code or JetBrains Mono 14–16pt, ~120 cols, padding 0, steady block cursor. One pinned tab, CWD = `C:\Users\Niels\experiments\weiner\shipment-agent`.
- [ ] **VS Code** profile: Dark+, Cascadia Code 14pt, minimap OFF, breadcrumb OFF, sticky scroll OFF. Workspace = `shipment-agent` only. Welcome tab closed. Only C# extension visible.
- [ ] **Browser (Cut 1)**: Chrome, single window, single tab, mock Outlook inbox page loaded. Bookmarks bar hidden.
- [ ] **`scripts/sample-request.json`** open in VS Code, one click away. Terminal width sized so the eight-step JSON fits without horizontal scroll.
- [ ] **`bash scripts/demo.sh` pre-run once**. Confirm eight steps render. Lock in the JSON shape — no surprises at record time.
- [ ] **OBS Studio 28+** installed. **Audacity** installed. **DaVinci Resolve** installed. All three launched at least once to confirm no first-run prompts.
- [ ] **Mic** (Fifine AM8 / Rode NT-USB Mini) plugged in, OS recognises it as default input. **Closed-back headphones** (Sony MDR-7506 or similar) on hand.
- [ ] **End-card art** drafted — single PNG, `github.com/ikkeflikkeri/shipment-agent` + `niels@wwx.be`, 1920x1080.
- [ ] **Recording path** exists: `C:\recordings\` with write access confirmed.
- [ ] **Script** printed on paper. **Phone** silenced and in another room.
- [ ] **One full dry run** end-to-end with no recording. Catch the surprises here, not at 00:07.

## 2. Day-of morning

- [ ] **Sleep**: actually sleep. Voice cracks at 03:14 otherwise.
- [ ] **Water within reach**. Coffee, not Red Bull.
- [ ] **Reboot**. Clears Teams residue, resets mic enumeration.
- [ ] **Confirm mic in OBS**: Settings → Audio → Mic/Auxiliary Audio → your USB condenser. Sample rate 48 kHz.
- [ ] **Confirm headphones**: closed-back on. Live monitoring cable if available.
- [ ] **Audacity open**: input meter showing. Do a 10-second test recording, speak normally, play back. **Peaks ~ -12 dB, no clipping**. Adjust gain on the mic, not in software.
- [ ] **OBS settings**:
  - Settings → Video: Base canvas 1920x1080, output 1920x1080, 30 fps.
  - Settings → Output → Recording: Type = Standard, Encoder = x264, Rate control = CQP, CQ level 18–20, Format = **mp4**, Recording path = `C:\recordings\shipment-agent-take-N.mp4`.
  - Audio: track 1 = mic (48 kHz, 160 kbps AAC).
  - Scene: single Display Capture, full screen. No webcam. No alerts.
- [ ] **Hit Record for 5 seconds, stop, play back**. Confirm the file lands in `C:\recordings\` and plays cleanly.
- [ ] **Chrome**: only the mock Outlook tab open. Other windows closed. No notifications.
- [ ] **VS Code**: only `shipment-agent` workspace, only `sample-request.json` open.
- [ ] **Terminal**: only the pinned Git Bash tab on `shipment-agent`.
- [ ] **Printout of this checklist** on the desk.

## 3. Setup phase (00:00 → 00:10)

- [ ] **00:00 → 00:02** — Sit down. Read script once aloud, cold, no recording. Find the phrasing.
- [ ] **00:02 → 00:04** — **Arm OBS**: click "Start Recording". Confirm the timer is counting. Do not stop it until end-of-session export phase.
- [ ] **00:04 → 00:06** — **VO dry run**: switch to Audacity, record a 30-second VO test for Cut 1. Play back. Confirm levels, no clipping, no background hum. Reset position, don't save.
- [ ] **00:06 → 00:08** — **Screen dry run**: in OBS preview, run `bash scripts/demo.sh` once silently. Confirm the eight steps render. Reset terminal state (clear screen is fine, the JSON shape is locked).
- [ ] **00:08 → 00:10** — **Mark t=0**: announce to camera (or to the room): "Rolling at take 1." Switch mental mode from setup → record. Begin Cut 1.

## 4. Take-by-take shot list

Reference: §2 of `docs/video-producer-plan.md` for full visuals.

### Cut 1 — Problem (0:00 → 0:30)

- **Takes planned**: 2.
- **Take 1**:
  - Chrome full-screen on mock Outlook inbox. 6–8 unread shipment emails. Read VO cold from script. 15s.
  - Hard cut to terminal. Type `bash scripts/demo.sh`, leave cursor blinking. 5s.
  - Black/silence beat. VO: "The shape of this work is exactly what an agent is for." 5s. +5s buffer.
- **Take 2**: same. Use only if take 1 has a flub or a mic pop.
- **Recovery if it breaks**: terminal typing flub → cut to a clean prompt, retype. Email count wrong in the mock → re-shoot the 15s beat, splice in. Don't restart the cut for one pause-um.

### Cut 2 — Agent (0:30 → 2:30)

- **Takes planned**: 3. Host must be frozen between takes so the JSON matches.
- **Take 1**:
  - `bash scripts/demo.sh`. curl hits host. JSON streams in. 5s.
  - Open `scripts/sample-request.json`. Show `{ "orderId": "ORD-12345", … }`. VO: "One order id. That's it." 5s.
  - Full JSON response on screen, hold. 20s.
  - Slow cursor walk through the eight steps: ERP → CRM → Inventory → Carrier → CRM → ERP → CRM. VO lists each. 40s.
  - Pause on tracking reference block. VO: "the agent didn't just run a workflow, it produced a usable outcome." 15s. +5s buffer.
- **Take 2**: same. Use if cursor lost or VO stumble.
- **Take 3**: same. Use only if takes 1 and 2 are both bad — this is the safety.
- **Recovery if it breaks**:
  - JSON shape differs between takes → cut to the matching take only. Don't mix.
  - Cursor jumps past a step → re-shoot that 5s beat, splice in. Cheaper than restarting.
  - Terminal scrolls wrong → freeze the host (`Stop` the listener), restart cleanly, retake only the affected segment.

### Cut 3 — Stack (2:30 → 4:00)

- **Takes planned**: 2.
- **Take 1**:
  - VS Code Solution Explorer, `src/` expanded. Four projects visible. 5s.
  - Open `ShipmentAgent.Host/Program.cs` briefly. 8s.
  - Open `src/ShipmentAgent.Agents/ShipmentAgentRunner.cs`. Scroll through the eight step invocations. 25s.
  - `GenerateNotificationBodyAsync` — kernel call + fallback. 15s.
  - `src/ShipmentAgent.Tools/ICarrierTool.cs` — typed contract. 10s.
  - `src/ShipmentAgent.Infrastructure/ShipmentAgentOptions.cs` — config + telemetry. 7s.
  - `ShipmentAgentRunnerTests.cs` open. Integrated terminal → `dotnet test`. Green tick. 15s. +5s buffer.
- **Take 2**: same. Use if `dotnet test` is flaky or a code pan stutters.
- **Recovery if it breaks**:
  - `dotnet test` red → kill take, fix code, re-record **only** the test segment, splice in. Don't pretend it's green.
  - Wrong file open → cut, reopen, retake the 5–15s beat.
  - VS Code lag → pre-warm by opening each file in order during dry run (already done the night before).

### Cut 4 — Hire (4:00 → 5:00)

- **Takes planned**: 2 (one safety for end-card timing).
- **Take 1**:
  - Open `docs/portfolio.md`. Scroll through. 5s.
  - VO close over the markdown source. 40s.
  - End card, frozen. 15s.
- **Take 2**: same. Safety net.
- **Recovery if it breaks**:
  - End-card timing off → retake the 15s beat only. The 40s VO carries over.
  - VO flub near the close → punch-in fix in Audacity, lay the fixed stem in Resolve. Don't re-record the whole cut.

## 5. Voiceover checklist

Before each VO pass:

- [ ] **Audacity armed**, fresh project, mono track, 48 kHz / 24-bit.
- [ ] **Mic gain untouched since morning**. Don't touch it mid-session.
- [ ] **Room quiet**: HVAC off if possible, phone face-down, door closed.
- [ ] **Headphones on** so you hear plosives and sibilance live.
- [ ] **Water sip**, not a gulp.
- [ ] **Script on paper**, not on screen (screen rustling = noise).
- [ ] **Read the first line aloud once** before hitting record. Loosens the throat.
- [ ] **Hit record, wait one second, start speaking**. Trim the silence in post.
- [ ] **Peaks between -12 dB and -6 dB**. Lower than -18 dB sounds thin in Resolve's mix.
- [ ] **No retakes on a flubbed line mid-pass**. Pause, re-speak the line from the top. Edit the pause out later.
- [ ] **Save stem** as `vo-{cut}.wav` in the recording folder. One pass per cut, then punch-ins for flubs.
- [ ] **Stand up between cuts** if voice is fatiguing. 30 seconds reset.

## 6. Re-record decision rule

**Rule**: Re-shoot only the broken segment, never the whole cut, unless the JSON shape or code on screen is wrong — then the cut dies.

- **Re-shoot**: in Cut 2, the eight-step JSON comes back with a different shape than the dry-run JSON. The whole visual is the asset. Cut 2 is dead, retake it whole.
- **Push through**: in Cut 3, you fumble the `dotnet test` green tick on the first take. Don't apologise on camera. Retake only the 15-second test segment, splice in. The four other beats are fine, save them.

## 7. End-of-session export checklist (last 15 min)

- [ ] **OBS: Stop Recording**. File lands at `C:\recordings\shipment-agent-take-N.mp4`.
- [ ] **Confirm file size** > 50 MB. Smaller = capture glitched, re-record.
- [ ] **Play the MP4 from start to finish** in VLC or Resolve. Watch the timeline, not just the picture.
- [ ] **Import all VO stems** (`vo-problem.wav`, `vo-agent.wav`, `vo-stack.wav`, `vo-stack.wav`, `vo-hire.wav`) into Resolve.
- [ ] **Lay VO over visuals**, sync to script beats. Cross-fade cuts at scene boundaries.
- [ ] **End-card composited** as a 5s static frame at 4:55. PNG dropped on a clip in Resolve, baked into the timeline.
- [ ] **Captions**: either Resolve's subtitle tool, or upload draft to YouTube Studio, auto-caption, download `.srt`, re-embed as soft subs. **Soft subs preferred** over burned-in for accessibility.
- [ ] **Export settings**:
  - Format: **MP4**
  - Codec: **H.264**
  - Audio: **AAC stereo**
  - Resolution: **1920x1080 @ 30 fps**
  - Bitrate: **~12 Mbps**
- [ ] **Export to** `C:\recordings\shipment-agent-v1.mp4`.
- [ ] **Verify export**: play it back in a fresh window. Captions show. End-card at 4:55. Audio levels consistent across cuts (no sudden dips).
- [ ] **Backup copy** to a second drive or OneDrive before you close the laptop.
- [ ] **Stop OBS**, close Audacity, close Resolve. Session clean.

## 8. Post-session (within 24 hours)

- [ ] **Upload to YouTube** as **unlisted**. Title: `shipment-agent — 5-min demo`. Description: short summary + links. Thumbnail: the end-card frame.
- [ ] **Auto-captions** in YouTube Studio. Review, fix obvious errors. Save the corrected `.srt` and re-upload it.
- [ ] **Create GitHub release** `v1.0.0-demo`. Attach the MP4 as a release asset.
- [ ] **Update repo `README.md`**: embed `<video>` tag pointing at the release asset URL. Add the YouTube link under the demo.
- [ ] **LinkedIn post draft** in a notes file — link to YouTube, one-line context, tags relevant to ex-DU network. Schedule for a weekday morning, not Sunday night.
- [ ] **Delete dry-run MP4s** from `C:\recordings\` once the export is verified. Keep the final MP4 and all VO stems.
- [ ] **Note any flubs** you didn't catch for next time — voice crack at 02:14, cursor jump at 03:40, whatever. Future you will thank past you.
