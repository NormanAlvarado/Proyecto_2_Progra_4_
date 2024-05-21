using DataAcess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAcess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<MyDbContext>(options =>
            options.UseMySQL(
                configuration.
                GetConnectionString("MyDbContext")
                ?? throw new InvalidOperationException("Connection string 'MyDbContext' not found.")
                ));


            services.AddScoped<MyDbContext>();

            return services;
        }
    }
}