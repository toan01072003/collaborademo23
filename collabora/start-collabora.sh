#!/usr/bin/env bash
set -euo pipefail

: "${domain:=.+}"
: "${username:=admin}"
: "${password:=admin}"
: "${extra_params:=--o:ssl.enable=false}"

echo "[Collabora Startup]"
echo " domain=${domain}"
echo " extra_params=${extra_params}"
echo " username=${username}"

/usr/bin/coolwsd --version || true

exec /usr/bin/loolwsd-systemplate-setup.sh &&         /usr/bin/loolwsd-generate-proof-key.sh &&         /start-collabora-online.sh
