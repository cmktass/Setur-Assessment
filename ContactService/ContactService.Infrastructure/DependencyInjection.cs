using ContactService.Application;
using ContactService.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ContactDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IContactServiceDbContext, ContactDbContext>();
            return services;
        }
    }
}
