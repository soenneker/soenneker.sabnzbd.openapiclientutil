using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Sabnzbd.HttpClients.Registrars;
using Soenneker.Sabnzbd.OpenApiClientUtil.Abstract;

namespace Soenneker.Sabnzbd.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class SabnzbdOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="SabnzbdOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddSabnzbdOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddSabnzbdOpenApiHttpClientAsSingleton()
                .TryAddSingleton<ISabnzbdOpenApiClientUtil, SabnzbdOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="SabnzbdOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddSabnzbdOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddSabnzbdOpenApiHttpClientAsSingleton()
                .TryAddScoped<ISabnzbdOpenApiClientUtil, SabnzbdOpenApiClientUtil>();

        return services;
    }
}
