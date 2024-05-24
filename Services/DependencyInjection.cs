using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.IService;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
             services.AddScoped<IAppointmentService, AppointmentService>();
             services.AddScoped<IClinicService, ClinicService>();

            return services;
        }
    }
}