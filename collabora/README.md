# Collabora (CODE) for Railway

## Env Vars
- `domain` = regex of WOPI host origin. e.g. `^wopi-host-[a-z0-9-]+\.up\.railway\.app$`
- `username`, `password` (optional, admin)
- `extra_params` = `--o:ssl.enable=false`

Verify discovery:
`https://<your-collabora-service>.railway.app/hosting/discovery`
