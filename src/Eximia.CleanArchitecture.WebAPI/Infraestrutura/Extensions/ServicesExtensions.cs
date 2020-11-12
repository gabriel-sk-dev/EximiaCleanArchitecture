using Eximia.CleanArchitecture.Avaliacoes;
using Eximia.CleanArchitecture.WebAPI.Infraestrutura.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Eximia.CleanArchitecture.WebAPI.Infraestrutura.Extensions
{
    internal static class ServicesExtensions
    {
        public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
            => services.AddApplicationInsightsTelemetry(configuration);

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddControllers(options =>
                {
                    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                })
                .AddNewtonsoftJson()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                        builder
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .SetIsOriginAllowed((host) => true)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithOrigins(configuration.GetValue("OrigensPermitidas", "*")));
            });
            return services;
        }

        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();
            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder
                .AddSqlServer(
                    configuration["ConnectionStrings:Contexto"],
                    name: $"{Ambiente.AppName}-check",
                    tags: new string[] { $"{Ambiente.AppName}DbCheck" });
            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            //Adicionar servicos que usam HttpClient
            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            //Por exemplo para usar um serviço de mensageria como integrador
            //services.AddScoped<IServicoMensageria>(sp =>
            //{
            //    var logger = sp.GetRequiredService<ILoggerFactory>();
            //    var serviceBusConnection = new ServiceBusConnectionStringBuilder(configuration["ConnectionStrings:AzureServiceBus"]);
            //    return new AzureServiceBus(serviceBusConnection, logger);
            //});
            return services;
        }
    }
}
