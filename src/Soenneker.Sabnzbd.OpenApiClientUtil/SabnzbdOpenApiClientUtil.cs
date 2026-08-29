using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Sabnzbd.HttpClients.Abstract;
using Soenneker.Sabnzbd.OpenApiClient;
using Soenneker.Sabnzbd.OpenApiClientUtil.Abstract;
using Soenneker.Sabnzbd.OpenApiClientUtil.Authentication;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Sabnzbd.OpenApiClientUtil;

/// <inheritdoc cref="ISabnzbdOpenApiClientUtil"/>
public sealed class SabnzbdOpenApiClientUtil : ISabnzbdOpenApiClientUtil
{
    private readonly AsyncSingleton<SabnzbdOpenApiClient> _client;

    public SabnzbdOpenApiClientUtil(ISabnzbdOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<SabnzbdOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            string apiKey = configuration.GetStringStrict("Sabnzbd:ApiKey");
            Uri baseAddress = httpClient.BaseAddress ?? throw new InvalidOperationException("The SABnzbd HTTP client must have a base address.");

            var requestAdapter = new HttpClientRequestAdapter(new SabnzbdApiKeyAuthenticationProvider(apiKey, baseAddress), httpClient: httpClient);

            return new SabnzbdOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<SabnzbdOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
