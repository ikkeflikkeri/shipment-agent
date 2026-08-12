#!/usr/bin/env bash
# Drive the shipment-agent demo end-to-end against a running host.
set -euo pipefail

HOST="${HOST:-http://localhost:5000}"
REQUEST_FILE="$(dirname "$0")/sample-request.json"

echo "→ POST $HOST/shipments"
curl -sf -X POST "$HOST/shipments" \
  -H 'Content-Type: application/json' \
  --data-binary "@$REQUEST_FILE" | jq .