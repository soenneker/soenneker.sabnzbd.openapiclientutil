using System.Threading;
using System;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions;
using Soenneker.Sabnzbd.OpenApiClient;
using Soenneker.Sabnzbd.OpenApiClientUtil.Abstract;
using Soenneker.Sabnzbd.OpenApiClientUtil.Authentication;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Sabnzbd.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class SabnzbdOpenApiClientUtilTests : HostedUnitTest
{
    private readonly ISabnzbdOpenApiClientUtil _openapiclientutil;

    public SabnzbdOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<ISabnzbdOpenApiClientUtil>(true);
    }

    [Test]
    public async Task Authentication_provider_adds_apikey_query_parameter(CancellationToken cancellationToken)
    {
        var provider = new SabnzbdApiKeyAuthenticationProvider("test-api-key", new Uri("http://localhost:8080"));
        var request = new RequestInformation
        {
            UrlTemplate = "{+baseurl}/api{?mode*}"
        };
        request.PathParameters["baseurl"] = "http://localhost:8080";
        request.QueryParameters["mode"] = "version";

        await provider.AuthenticateRequestAsync(request, cancellationToken: cancellationToken);

        await Assert.That(request.URI.Query).Contains("mode=version");
        await Assert.That(request.URI.Query).Contains("apikey=test-api-key");
    }

    [Test]
    public async Task Authentication_provider_does_not_send_key_to_another_host(CancellationToken cancellationToken)
    {
        var provider = new SabnzbdApiKeyAuthenticationProvider("test-api-key", new Uri("http://localhost:8080"));
        var request = new RequestInformation
        {
            URI = new Uri("https://example.com/api?mode=version")
        };

        await provider.AuthenticateRequestAsync(request, cancellationToken: cancellationToken);

        await Assert.That(request.URI.Query).DoesNotContain("apikey");
    }

    [Test]
    public async Task Get_returns_generated_client(CancellationToken cancellationToken)
    {
        SabnzbdOpenApiClient client = await _openapiclientutil.Get(cancellationToken: cancellationToken);

        await Assert.That(client).IsNotNull();
    }
}
