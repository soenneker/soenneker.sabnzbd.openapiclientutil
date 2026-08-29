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
    /// <summary>
    /// Returns the configured sabnzbd OpenAPI Client used by the Sabnzbd OpenAPI Client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested sabnzbd OpenAPI Client.</returns>
    ValueTask<SabnzbdOpenApiClient> Get(CancellationToken cancellationToken = default);
}
