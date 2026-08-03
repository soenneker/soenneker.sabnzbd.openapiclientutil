[![](https://img.shields.io/nuget/v/soenneker.sabnzbd.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.sabnzbd.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.sabnzbd.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.sabnzbd.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.sabnzbd.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.sabnzbd.openapiclientutil/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Sabnzbd.OpenApiClientUtil
### A thread-safe utility for obtaining SABnzbd's OpenAPI client singleton.

## Installation

```
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

`ClientBaseUrl` is the SABnzbd instance root and should not end in `/api`. The API key is sent using SABnzbd's `apikey` query parameter.
