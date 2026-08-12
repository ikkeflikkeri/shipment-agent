#!/usr/bin/env bash
# Auto-render the shipment-agent demo as an MP4.
#
# Fallback render path — no screen recording, no presenter, no audio.
# Captures the eight-step audit trail visually with FFmpeg drawtext.
# The "live" recorded demo (with VO, presenter, terminal depth) is what
# ships first; this is the safety net.
#
# Output: video/auto/shipment-agent-demo.mp4 (1920x1080, H.264, 30s)

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
ROOT_DIR="$(cd "$SCRIPT_DIR/../.." && pwd)"
OUT_DIR="$SCRIPT_DIR"
CLIPS_DIR="$OUT_DIR/clips"

DEMO_PORT="${DEMO_PORT:-5099}"

cleanup() {
    if [[ -n "${HOST_PID:-}" ]]; then
        kill "$HOST_PID" 2>/dev/null || true
        wait "$HOST_PID" 2>/dev/null || true
    fi
}
trap cleanup EXIT

cd "$ROOT_DIR"

mkdir -p "$OUT_DIR" "$CLIPS_DIR"
rm -f "$CLIPS_DIR"/*.mp4 "$OUT_DIR"/shipment-agent-demo.mp4

echo "[1/6] Building solution (Release)"
if ! dotnet build --nologo -v quiet -c Release > "$OUT_DIR/build.log" 2>&1; then
    cat "$OUT_DIR/build.log"
    exit 1
fi

echo "[2/6] Booting host on :$DEMO_PORT"
ASPNETCORE_URLS="http://localhost:$DEMO_PORT" \
    dotnet "src/ShipmentAgent.Host/bin/Release/net10.0/ShipmentAgent.Host.dll" \
    > "$OUT_DIR/host.log" 2>&1 &
HOST_PID=$!

# Wait for /health (no auth on the demo host).
for _ in $(seq 1 60); do
    if curl -sf "http://localhost:$DEMO_PORT/health" > /dev/null 2>&1; then
        break
    fi
    sleep 0.5
done
if ! curl -sf "http://localhost:$DEMO_PORT/health" > /dev/null 2>&1; then
    echo "Host failed to start. See $OUT_DIR/host.log"
    exit 1
fi

echo "[3/6] POSTing shipment request"
curl -sf -X POST "http://localhost:$DEMO_PORT/shipments" \
    -H 'Content-Type: application/json' \
    --data-binary "@scripts/sample-request.json" \
    -o "$OUT_DIR/outcome.json"

echo "[4/6] Killing host"
cleanup

echo "[5/6] Rendering scene clips with FFmpeg"
# Find a monospace font available on this machine.
FONT=""
for candidate in \
    "/c/Windows/Fonts/consola.ttf" \
    "/c/Windows/Fonts/CascadiaMono.ttf" \
    "/c/Windows/Fonts/cour.ttf" \
    "/usr/share/fonts/truetype/dejavu/DejaVuSansMono-Bold.ttf"; do
    if [[ -f "$candidate" ]]; then
        FONT="$candidate"
        break
    fi
done
if [[ -z "$FONT" ]]; then
    echo "No monospace font found."
    exit 1
fi

# Pull the data out of the captured outcome.
OUTCOME_JSON="$(cat "$OUT_DIR/outcome.json")"
read_tracking=$(echo "$OUTCOME_JSON" | jq -r '.trackingReference')
read_carrier=$(echo "$OUTCOME_JSON"  | jq -r '.carrierName')
read_pickup=$(echo "$OUTCOME_JSON"   | jq -r '.confirmedPickupDate')
read_eta=$(echo "$OUTCOME_JSON"      | jq -r '.estimatedDeliveryDate')
step_count=$(echo "$OUTCOME_JSON"    | jq -r '.steps | length')

# Build step lines for the steps scene (one line per audit step).
STEPS_TXT="$CLIPS_DIR/_steps.txt"
: > "$STEPS_TXT"
for i in $(seq 0 $((step_count - 1))); do
    name=$(echo "$OUTCOME_JSON" | jq -r ".steps[$i].name")
    outcome=$(echo "$OUTCOME_JSON" | jq -r ".steps[$i].outcome")
    # Truncate long outcomes so the line fits the canvas.
    outcome_short=$(printf '%s' "$outcome" | cut -c1-70)
    printf '%-22s | %s\n' "$name" "$outcome_short" >> "$STEPS_TXT"
done

# Helper: escape text for FFmpeg drawtext (colons, single quotes).
escape_drawtext() {
    # Replace : with \: and ' with \'  per FFmpeg drawtext rule, and
    # also strip newlines.
    local s="$1"
    s="${s//\\/\\\\}"
    s="${s//:/\\:}"
    s="${s//\'/\\\'}"
    printf '%s' "$s"
}

render_scene() {
    local out="$1" duration="$2"
    shift 2
    local filters=""

    # Background colour source first. Duration is on the source, not the
    # drawtext filters.
    filters="color=c=#0d1117:s=1920x1080:rate=30:d=${duration}"

    local y=120
    for pair in "$@"; do
        local text="$pair"
        text="$(escape_drawtext "$text")"
        filters+=",drawtext=fontfile=${FONT}:fontcolor=#e6edf3:fontsize=46:text='${text}':x=160:y=${y}"
        y=$((y + 70))
    done

    ffmpeg -y -loglevel error \
        -f lavfi -i "$filters" \
        -c:v libx264 -pix_fmt yuv420p -preset slow -crf 18 \
        "$out"
}

# Scene 1 — title card (3s)
echo "  - 01-title"
render_scene "$CLIPS_DIR/01-title.mp4" 3 \
    "shipment-agent" \
    "demo" \
    "" \
    "github.com/ikkeflikkeri/shipment-agent"

# Scene 2 — request (3s)
echo "  - 02-request"
REQUEST_BODY=$(cat scripts/sample-request.json | tr -d '\n' | cut -c1-100)
render_scene "$CLIPS_DIR/02-request.mp4" 3 \
    "POST /shipments" \
    "" \
    "$REQUEST_BODY" \
    "" \
    "[start]"

# Scene 3 — eight-step audit trail (3s per step, total ~24s)
echo "  - 03-steps"
STEPS_ARGS=()
while IFS= read -r line; do
    [[ -z "$line" ]] && STEPS_ARGS+=("") && continue
    STEPS_ARGS+=("$line")
done < "$STEPS_TXT"
render_scene "$CLIPS_DIR/03-steps.mp4" 24 "${STEPS_ARGS[@]}"

# Scene 4 — outcome / tracking (4s)
ORDER_ID=$(echo "$OUTCOME_JSON" | jq -r '.orderId')
SHIPMENT_ID=$(echo "$OUTCOME_JSON" | jq -r '.shipmentId')
echo "  - 04-outcome"
render_scene "$CLIPS_DIR/04-outcome.mp4" 4 \
    "Outcome" \
    "" \
    "Order         $ORDER_ID" \
    "Shipment ID   $SHIPMENT_ID" \
    "Carrier       $read_carrier" \
    "Tracking      $read_tracking" \
    "Pickup        $read_pickup" \
    "ETA           $read_eta"

# Scene 5 — repo link (3s)
echo "  - 05-end"
render_scene "$CLIPS_DIR/05-end.mp4" 3 \
    "github.com/ikkeflikkeri/shipment-agent" \
    "" \
    "Auto-rendered. No voiceover." \
    "Sibling: shipment-agent-platform" \
    "(Aspire · Azure AI Search · Entra ID)"

echo "[6/6] Concatenating scene clips"
# Build concat list
LIST="$CLIPS_DIR/list.txt"
: > "$LIST"
for f in 01-title 02-request 03-steps 04-outcome 05-end; do
    echo "file '${f}.mp4'" >> "$LIST"
done

ffmpeg -y -loglevel error \
    -f concat -safe 0 -i "$LIST" \
    -c:v libx264 -pix_fmt yuv420p -preset slow -crf 18 \
    -movflags +faststart \
    "$OUT_DIR/shipment-agent-demo.mp4"

echo ""
echo "Done: $OUT_DIR/shipment-agent-demo.mp4"
ls -lh "$OUT_DIR/shipment-agent-demo.mp4"