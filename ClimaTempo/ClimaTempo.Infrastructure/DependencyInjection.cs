using ClimaTempo.Domain.Interfaces;
using ClimaTempo.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ClimaTempo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<ICidadeFavoritaRepository, CidadeFavoritaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            return services;
        }
    }
}
