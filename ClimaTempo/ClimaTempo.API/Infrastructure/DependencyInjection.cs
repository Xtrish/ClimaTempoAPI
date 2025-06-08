using ClimaTempo.API.Domain.Interfaces;
using ClimaTempo.API.Repositories;

namespace ClimaTempo.API.Infrastructure
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
