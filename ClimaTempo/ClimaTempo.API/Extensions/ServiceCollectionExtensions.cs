using FluentValidation;
using ClimaTempo.Application;

namespace ClimaTempo.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
            return services;
        }
    }
}
