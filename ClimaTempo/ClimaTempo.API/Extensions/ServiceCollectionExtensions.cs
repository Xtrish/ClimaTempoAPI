using FluentValidation;
using System.Reflection;

namespace ClimaTempo.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.Load("ClimaTempo.Application"));
            return services;
        }
    }
}
