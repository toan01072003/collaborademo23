# Collabora Online + .NET 5 WOPI Host — Railway Starter

Monorepo with two services:
- `wopi-host`: ASP.NET 5 WOPI-like host + tiny UI
- `collabora`: Collabora Online (CODE) container

## Deploy (Railway)
1. Create project → Deploy from Repo.
2. Two services detected via `Railway.toml` (or add manually).
3. After deploy, set service variables:
   - **wopi-host**: `COLLABORA__BASEURL`, `APP__BASEURL`
   - **collabora**: `domain` (tight regex of wopi-host domain)
4. Open the wopi-host URL → click **Open in Collabora**.

## Local Dev (optional)
- Run Collabora locally (port 9980) and set env in launchSettings.json.
- Run `dotnet run` in `wopi-host` (listens on :8080).
# collaborademo23
