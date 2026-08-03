using Soenneker.Sabnzbd.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Sabnzbd.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface ISabnzbdOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    ValueTask<SabnzbdOpenApiClient> Get(CancellationToken cancellationToken = default);
}
