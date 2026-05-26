#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
BACKEND_DIR="$ROOT_DIR/backend"

backend_pid=""
frontend_pid=""

require_free_port() {
  local port=$1

  if lsof -iTCP:"$port" -sTCP:LISTEN >/dev/null 2>&1; then
    echo "Port $port is already in use. Stop the existing process first." >&2
    exit 1
  fi
}

cleanup() {
  local exit_code=$?

  if [[ -n "$backend_pid" ]] && kill -0 "$backend_pid" 2>/dev/null; then
    kill "$backend_pid" 2>/dev/null || true
  fi

  if [[ -n "$frontend_pid" ]] && kill -0 "$frontend_pid" 2>/dev/null; then
    kill "$frontend_pid" 2>/dev/null || true
  fi

  wait 2>/dev/null || true
  exit "$exit_code"
}

trap cleanup EXIT INT TERM

require_free_port 8003
require_free_port 3004

cd "$ROOT_DIR"
dotnet run --project "$BACKEND_DIR/LogisticsApi.csproj" &
backend_pid=$!

npm run serve -- --port 3004 &
frontend_pid=$!

wait "$backend_pid" "$frontend_pid"
