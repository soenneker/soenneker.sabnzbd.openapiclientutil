[![](https://img.shields.io/nuget/v/soenneker.sabnzbd.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.sabnzbd.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.sabnzbd.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.sabnzbd.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.sabnzbd.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.sabnzbd.openapiclientutil/)

# Soenneker.Sabnzbd.OpenApiClientUtil

Exposes a cached OpenAPI client instance.

## Install

```bash
dotnet add package Soenneker.Sabnzbd.OpenApiClientUtil
```

## Quick start

```csharp
using Soenneker.Sabnzbd.OpenApiClientUtil.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddSabnzbdOpenApiClientUtilAsSingleton();
```

Adds `SabnzbdOpenApiClientUtil` as a singleton service.

## What you get

- `ISabnzbdOpenApiClientUtil` — Exposes a cached OpenAPI client instance.
- `SabnzbdOpenApiClientUtilRegistrar` — Registers the OpenAPI client utility for dependency injection.
- `SabnzbdApiKeyAuthenticationProvider` — Adds a SABnzbd API key to Kiota requests as the `apikey` query parameter.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `SabnzbdOpenApiClientUtilRegistrar.AddSabnzbdOpenApiClientUtilAsSingleton(services)` | Adds `SabnzbdOpenApiClientUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `SabnzbdOpenApiClientUtilRegistrar.AddSabnzbdOpenApiClientUtilAsScoped(services)` | Adds `SabnzbdOpenApiClientUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Dispose instances you own when their scope ends so held resources can be released.
