## Certificate

```bash
dotnet cake -target=Publish
docker-compose build
docker-compose up
open https://localhost:8001/api/values
```

## Developement

```bash
dotnet dev-certs https -v -ep settings/cert-aspnetcore.pfx -p 1234
```