# Auto-rendered demo

`shipment-agent-demo.mp4` is a fallback render of the demo. It has no
voiceover and no presenter; FFmpeg + drawtext renders the eight-step
audit trail against a dark background.

This file is the safety net — it's always regenerable from
`render.sh` and lands in the README even if the live recording session
never happens.

## Regenerate

```bash
bash video/auto/render.sh
```

Requires `dotnet`, `jq`, and `ffmpeg` on PATH.

## What it shows

- Title card with repo URL
- The exact `POST /shipments` payload from `scripts/sample-request.json`
- All eight audit steps with name + outcome (the wow moment)
- The outcome block: order id, shipment id, carrier, tracking, pickup, ETA
- Closing card with the platform repo sibling

## Comparison to the live take

| | Auto | Live |
| --- | --- | --- |
| Voiceover | none | full |
| Duration | 37s | 5 min |
| Visual | static text | presenter + screen recording |
| Purpose | fallback / link preview | primary deliverable |

Use the auto-render as the `<video>` source in the README until the live
take is recorded. After the live take ships, point at YouTube unlisted
and link the auto-render under "Older preview" or similar.