[![](https://img.shields.io/nuget/v/soenneker.sabnzbd.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.sabnzbd.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.sabnzbd.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.sabnzbd.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.sabnzbd.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.sabnzbd.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.sabnzbd.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.sabnzbd.openapiclientutil/actions/workflows/codeql.yml)

# Soenneker.Sabnzbd.OpenApiClientUtil

A DI-ready, cached SABnzbd OpenAPI client with API-key authentication.

## Installation

```bash
dotnet add package Soenneker.Sabnzbd.OpenApiClientUtil
```

## Configuration

```json
{
  "Sabnzbd": {
    "ClientBaseUrl": "http://localhost:8080",
    "ApiKey": "your-api-key"
  }
}
```

`Sabnzbd:ApiKey` is required. `Sabnzbd:ClientBaseUrl` must be an absolute URI and defaults to `http://localhost:8080` when omitted.

## Registration

```csharp
using Soenneker.Sabnzbd.OpenApiClientUtil.Registrars;

services.AddSabnzbdOpenApiClientUtilAsSingleton();
```

For a scoped consumer, register a scoped utility:

```csharp
services.AddSabnzbdOpenApiClientUtilAsScoped();
```

The scoped registration deliberately retains the singleton HTTP client provider and transport. Disposing the utility at the end of a scope clears that utility's cached generated client, but does not destroy the underlying shared `HttpClient`.

## Usage

```csharp
using Soenneker.Sabnzbd.OpenApiClient;
using Soenneker.Sabnzbd.OpenApiClient.Models;
using Soenneker.Sabnzbd.OpenApiClientUtil.Abstract;

public sealed class SabnzbdHistoryReader(ISabnzbdOpenApiClientUtil clientUtil)
{
    public async Task<ApiCommandResponse?> GetHistory(CancellationToken cancellationToken)
    {
        SabnzbdOpenApiClient client = await clientUtil.Get(cancellationToken);

        return await client.Api.GetAsync(request =>
        {
            request.QueryParameters.Mode = Mode.History;
            request.QueryParameters.Output = Output.Json;
            request.QueryParameters.Limit = 50;
        }, cancellationToken);
    }
}
```

The generated client is created lazily on the first `Get` call and then reused by that utility instance. Configuration is read when the client is created; recreate the utility if the base URL or API key changes.

The authentication provider adds `apikey` only when the resolved request scheme and authority match the configured SABnzbd base address. It does not add the key to a request built with a URL on another host.
