using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ClimaTempo.Infrastructure.MediaR
{
    public static class MediatRInjection
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
