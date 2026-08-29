using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;

namespace Soenneker.Sabnzbd.OpenApiClientUtil.Authentication;

/// <summary>
/// Adds a SABnzbd API key to Kiota requests as the <c>apikey</c> query parameter.
/// </summary>
public sealed class SabnzbdApiKeyAuthenticationProvider : IAuthenticationProvider
{
    private readonly string _apiKey;
    private readonly string _allowedScheme;
    private readonly string _allowedAuthority;

    public SabnzbdApiKeyAuthenticationProvider(string apiKey, Uri baseAddress)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);
        ArgumentNullException.ThrowIfNull(baseAddress);

        _apiKey = apiKey;
        _allowedScheme = baseAddress.Scheme;
        _allowedAuthority = baseAddress.Authority;
    }

    /// <summary>
    /// Authenticates request Async for the Sabnzbd API Key Authentication Provider.
    /// </summary>
    /// <param name="request">request that defines the request to send.</param>
    /// <param name="additionalAuthenticationContext">additional Authentication Context to process.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the authenticate request async operation is complete.</returns>
    public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        Uri uri = request.URI;

        if (!string.Equals(uri.Scheme, _allowedScheme, StringComparison.OrdinalIgnoreCase) ||
            !string.Equals(uri.Authority, _allowedAuthority, StringComparison.OrdinalIgnoreCase))
            return Task.CompletedTask;

        var builder = new UriBuilder(uri);
        string query = uri.Query.TrimStart('?');
        string apiKeyParameter = $"apikey={Uri.EscapeDataString(_apiKey)}";

        builder.Query = string.IsNullOrEmpty(query) ? apiKeyParameter : $"{query}&{apiKeyParameter}";
        request.URI = builder.Uri;

        return Task.CompletedTask;
    }
}
