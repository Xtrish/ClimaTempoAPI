using ClimaTempo.API.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace ClimaTempo.API.Infrastructure
{
    public static class DatabaseInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
