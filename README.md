## Certificate

- [ASP.NET Core 2.1 application in Docker with HTTPS enabled](http://parsstudent.com/asp-net-core-2-1-application-in-docker-with-https-enabled)
- [HSTS policy with ASP.Net Core 2.1](http://parsstudent.com/hsts-policy-asp-net-core-2)

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