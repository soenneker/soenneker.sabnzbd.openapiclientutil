using Soenneker.Sabnzbd.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Sabnzbd.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides a lazily created, cached SABnzbd OpenAPI client.
/// </summary>
public interface ISabnzbdOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the authenticated SABnzbd OpenAPI client for this utility instance.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The cached generated client.</returns>
    ValueTask<SabnzbdOpenApiClient> Get(CancellationToken cancellationToken = default);
}
