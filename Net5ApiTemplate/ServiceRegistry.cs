using System.CodeDom.Compiler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore5ApiTemplate.Attributes;
using NetCore5ApiTemplate.Objects;
using Scrutor;

namespace NetCore5ApiTemplate
{
    public static class ServiceRegistry
    {
        public static void ScanForAllRemainingRegistrations(IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(Startup))
                .AddClasses(x => x.WithoutAttribute(typeof(GeneratedCodeAttribute)).WithoutAttribute(typeof(ScrutorIgnoreAttribute)))
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        public static void AddConfigs(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(option => { configuration.GetSection(nameof(AppSettings)).Bind(option); });
        }
    }
}
